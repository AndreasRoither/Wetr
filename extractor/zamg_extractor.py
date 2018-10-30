import csv
import os


with open('Stationsliste.csv', 'r', encoding='cp1252') as csvfile:
    reader = csv.reader(csvfile, delimiter=';')

    file = open("extract.txt","w") 
    file.write("INSERT INTO station (stationId, name, longitude, latitude, stationTypeId, addressId) VALUES\n")

    idCounter = 1

    for row in reader:
        stationType = 1
        if row[7] == "TAWES/VAMES":
            stationType=2
        elif row[7] == "TAWES":
            stationType=1
        else:
            stationType="1"
        file.write("\t(" + str(idCounter) + ", \"" + row[1] + "\", " + row[8] + ", " + row[9] + ", " + str(stationType) + ", 1" + "),\n")
        idCounter += 1