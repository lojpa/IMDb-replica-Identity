# IMDb-replica-Identity

Title
IMDb replica identity server

URL

https://localhost:44364/connect/token

Method:

GET

URL Params

Required:

headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

body: {'username': username, 'password': password, 'grant_type': 'password', 'client_id': 'angular.client', 'client_secret': 'secret'}

Success Response:

Code: 200 
Content: [{token: some-token}]

Notes:

IMPORTANT:
1. Clone or download -> Download ZIP
2. Open downloaded project via Visual Studio 
3. Wait untill VS load project and get necessary dependencies.
4. Run project
