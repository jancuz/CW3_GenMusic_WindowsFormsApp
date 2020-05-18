import subprocess
import os

# каталог mid
file_from_convert = "D:/3course/CW/CW3_GenMusic_WindowsFormsApp/CW3_GenMusic_WindowsFormsApp/bin/Debug/curPopulationMIDI"
# каталог wav
file_converted = "D:/3course/CW/CW3_GenMusic_WindowsFormsApp/CW3_GenMusic_WindowsFormsApp/bin/Debug/curPopulationWAV"
for songname_mid in os.listdir('{}'.format(file_from_convert)):
    songname_wav = songname_mid.replace('.mid', '.wav')
    print(songname_wav)
    subprocess.call(r"start C:\msys64\home\Яна\fluidsynth-2.1.2\build\src\fluidsynth.exe -ni D:/projectML/default_sound_font.sf2 {}/{} -F {}/{} -r 44100".format(file_from_convert, songname_mid, file_converted, songname_wav), shell=True)