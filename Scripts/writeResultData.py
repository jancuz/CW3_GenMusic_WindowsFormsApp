import csv
import numpy as np

file_from_r = "./Data/data.csv"
file_to_w = "./Data/dataResult.csv"

i = 1
with open(file_from_r, "r") as file_r:
    reader = csv.reader(file_r)
    for row in reader:
        if i != 1:
            with open(file_to_w, "a") as file_w:
                writer = csv.writer(file_w)
                writer.writerow(row)
                print(row)
        else:
            i += 1