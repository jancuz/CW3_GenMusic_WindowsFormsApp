import librosa
import csv
import os
import numpy as np

header = 'filename chroma_stft spectral_centroid spectral_bandwidth rolloff zero_crossing_rate'
for i in range(1, 21):
    header += ' mfcc{}'.format(i)
header = header.split()

file_to_w = "./Data/data.csv"
file_from_r = "./curPopulationWAV"

file = open(file_to_w, 'w')
with file:
    writer = csv.writer(file)
    writer.writerow(header)

for filename in os.listdir('{}'.format(file_from_r)):
    songname = '{}/{}'.format(file_from_r, filename)
    y, sr = librosa.load(songname, mono=True, duration=30)
    chroma_stft = librosa.feature.chroma_stft(y=y, sr=sr)
    spec_cent = librosa.feature.spectral_centroid(y=y, sr=sr)
    spec_bw = librosa.feature.spectral_bandwidth(y=y, sr=sr)
    rolloff = librosa.feature.spectral_rolloff(y=y, sr=sr)
    zcr = librosa.feature.zero_crossing_rate(y)
    mfcc = librosa.feature.mfcc(y=y, sr=sr)
    to_append = '{} {} {} {} {} {}'.format(
        filename,
        np.mean(chroma_stft),
        np.mean(spec_cent),
        np.mean(spec_bw),
        np.mean(rolloff),
        np.mean(zcr)
        )
    for e in mfcc:
        to_append += ' {}'.format(np.mean(e))
    file = open(file_to_w, 'a')
    with file:
        writer = csv.writer(file)
        writer.writerow(to_append.split())
