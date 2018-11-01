import csv
import os
import string
import re
from geopy.geocoders import Nominatim

provinceIdDictionary = {
"Burgenland" :1,
"Kärnten" :2,
"Niederösterreich" :3,
"Oberösterreich" :4,
"Salzburg" :5,
"Steiermark" :6,
"Tirol" :7,
"Vorarlberg" :8,
"Wien" :9
}

districtIdDictionary = {
"Eisenstadt" :1,
"Eisenstadt-Umgebung":2,
"Güssing":3,
"Jennersdorf":4,
"Mattersburg":5,
"Neusiedl am See":6,
"Neusiedl":6,
"Oberpullendorf":7,
"Oberwart":8,
"Rust":9,
"Klagenfurt am Wörthersee":1,
"Klagenfurt-Land":2,
"Feldkirchen":3,
"Hermagor":4,
"Sankt Veit an der Glan":5,
"St. Veit an der Glan":5,
"Spittal an der Drau":6,
"Villach":7,
"Villach-Land":8,
"Völkermarkt":9,
"Wolfsberg":10,
"Amstetten":1,
"Baden":2,
"Bruck an der Leitha":3,
"Gänserndorf":4,
"Gmünd":5,
"Hollabrunn":6,
"Horn":7,
"Korneuburg":8,
"Krems":9,
"Krems an der Donau":10,
"Lilienfeld":11,
"Melk":12,
"Mistelbach":13,
"Mödling":14,
"Neunkirchen":15,
"Sankt Pölten":16,
"St. Pölten":16,
"Scheibbs":17,
"Tulln":18,
"Waidhofen an der Thaya":19,
"Waidhofen an der Ybbs":20,
"Wiener Neustadt":21,
"Statuarstadt Wiener Neustadt":21,
"Zwettl":22,
"Braunau am Inn":1,
"Eferding":2,
"Freistadt":3,
"Gmunden":4,
"Grieskirchen":5,
"Kirchdorf an der Krems":6,
"Linz-Land":7,
"Linz":7,
"Perg":8,
"Ried im Innkreis":9,
"Rohrbach":10,
"Schärding":11,
"Steyr-Land":12,
"Urfahr-Umgebung":13,
"Vöcklabruck":14,
"Wels-Land":15,
"Salzburg-Stadt":1,
"Hallein":2,
"Salzburg-Umgebung":3,
"St. Johann im Pongau":4,
"Sankt Johann im Pongau":4,
"Tamsweg":5,
"Tamsweg - Lungau":5,
"Zell am See":6,
"Bruck-Mürzzuschlag":1,
"Deutschlandsberg":2,
"Graz":3,
"Graz-Umgebung":4,
"Hartberg-Fürstenfeld":5,
"Leibnitz":6,
"Leoben":7,
"Liezen":8,
"Murau":9,
"Murtal":10,
"Südoststeiermark":11,
"Voitsberg":12,
"Weiz":13,
"Imst":1,
"Innsbruck-Stadt":2,
"Innsbruck-Land":3,
"Kitzbühel":4,
"Kufstein":5,
"Landeck":6,
"Reutte":7,
"Reutte (Außerfern)":7,
"Schwaz":8,
"Lienz":9,
"Bregenz":1,
"Dornbirn":2,
"Feldkirch":3,
"Bludenz":4,
"Innere Stadt":1,
"Leoppoldstadt":2,
"Landstraße":3,
"Wieden":4,
"Margareten":5,
"Mariahilf":6,
"Neubau":7,
"Josefstadt":8,
"Alsergrund":9,
"Favoriten":10,
"Simmering":11,
"Meidling":12,
"Hietzing":13,
"Penzing":14,
"Rudolfsheim-Fünfhaus":15,
"Ottakring":16,
"Hernals":17,
"Währing":18,
"Döbling":19,
"Brigittenau":20,
"Floridsdorf":21,
"Donaustadt":22,
"Liesing":23,
}

communityDictionary = {}

with open('Stationsliste.csv', 'r', encoding='cp1252') as csvfile:
    reader = csv.reader(csvfile, delimiter=';')
    geolocator = Nominatim(user_agent="ZAMG")

    file = open("station.txt","w") 
    file2 = open("address.txt", "w")
    file3 = open("community.txt", "w")

    file.write("INSERT INTO station (stationId, name, longitude, latitude, stationTypeId, addressId, userId) VALUES\n")
    file2.write("INSERT INTO address (addressId, communityId, location, zip) VALUES\n")
    file3.write("INSERT INTO community (communityId, districtId, name) VALUES\n")

    idCounter = 1
    idCommunity = 1
    idUser = 1

    for row in reader:
        stationType = ""
        if row[7] == "TAWES/VAMES":
            stationType="2"
        elif row[7] == "TAWES":
            stationType="1"
        else:
            stationType="1"
        
        # Get row details
        name = row[1]
        longitude = row[8].replace(',', '.')
        latitude = row[9].replace(',', '.')
        locationString = latitude + ", " + longitude

        # Get location
        reverseLocation = geolocator.reverse(locationString)
        reverseLocationList = reverseLocation.address.split(',')
        length = len(reverseLocationList) - 1

        # Get location details
        plz = reverseLocationList[length-1].strip()
        province = reverseLocationList[length-2].replace(",", "").strip()
        district = reverseLocationList[length-3].replace(",", "").replace("Bezirk", "").strip()
        community = reverseLocationList[length-4].replace(",", "").replace("Gemeinde", "").strip()
        rest = ""
        for x in range (0, length-2):
            rest += reverseLocationList[x]

        doubleCommunity = True

        # Add Community if not in Dictionary
        if not community in communityDictionary:
            communityDictionary[community] = idCommunity
            doubleCommunity = False

        foundId = False
        
        provinceId = 1
        districtId = 1

        if district in districtIdDictionary and province in provinceIdDictionary:
            provinceId = provinceIdDictionary[province]
            districtId = districtIdDictionary[district]
            foundId = True
        # If order is wrong
        elif community in districtIdDictionary and province in provinceIdDictionary:
            try:
                provinceId = provinceIdDictionary[province]
                districtId = districtIdDictionary[community]
                foundId = True
            except:
                foundId = False
            

        # Check if province is in our Dictionary
        if foundId:
            # Values string

            if not doubleCommunity:
                valuesCommunity = "\t(" + str(idCommunity) + ", " + str(districtId) +  ", \"" + community + "\"),\n"
            valuesAddress = "\t(" + str(idCounter) + ", " + str(idCommunity) + ", \"" + rest + "\", " + str(plz) + "),\n"
            valuesStation = "\t(" + str(idCounter) + ", \"" + name + "\", " + longitude + ", " + latitude + ", " + stationType + ", " + str(idCounter) + ", " + str(idUser) + "),\n"

            # Write to files
            file2.write(valuesAddress)
            file.write(valuesStation)

            if not doubleCommunity:
                file3.write(valuesCommunity)
                idCommunity += 1
                idUser = (idUser+1)%26
                if idUser == 0:
                    idUser = 1
            
            print(str(idCounter) + " done")
            idCounter += 1
        else:
            print("Skipped: " + community + ", " + district + ", " + province + ", " + str(plz))