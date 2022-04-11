import datetime
from random import weibullvariate
from googleapiclient.discovery import build
from google.oauth2 import service_account
SCOPES = ['https://www.googleapis.com/auth/spreadsheets']

SERVICE_ACCOUNT_FILE = "C:/Program Files/Autodesk/Navisworks Manage 2017/Plugins/Bim Inventory Monitor Plugin/keys.json"
creds = None
creds = service_account.Credentials.from_service_account_file(SERVICE_ACCOUNT_FILE, scopes=SCOPES)
SAMPLE_SPREADSHEET_ID = '1xGDVIi0NeXvHeUCW0axROQHNACDyVzneEQi-Jcj6SZk'
SAMPLE_RANGE_NAME = 'Sheet1!A:E'

service = build('sheets', 'v4', credentials=creds)

    # Call the Sheets API
sheet = service.spreadsheets()
result = sheet.values().get(spreadsheetId=SAMPLE_SPREADSHEET_ID,range=SAMPLE_RANGE_NAME).execute()
values = result.get('values',[])
n=0; # ITeration Variable 

for z in values:
    n=n+1

#GETTING LAST VALUE
print("Last Value of the Sheet")
print(values[n-1])
lastValue = values[n-1]
#Converting Into Required Variables
Date = lastValue[0]
Time = lastValue[1]
Weight = lastValue[2]
try:
    Weight = float(Weight)
except:
    print("Value in Not A Float")
Weight = abs(Weight)
DateandTime = Date+" "+Time
f= open("values.txt","w+")
Weight = str(Weight)
Data = DateandTime+"\n" +Weight
f.write(Data)
f.close()