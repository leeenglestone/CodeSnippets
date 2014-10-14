import requests
r = requests.get("http://www.kitbag.com")
print(r.content)