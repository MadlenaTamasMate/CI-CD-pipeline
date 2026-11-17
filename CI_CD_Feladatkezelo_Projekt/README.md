# CI/CD beadandó – Feladatkezelő

ASP.NET Core 8 mini webalkalmazás, xUnit tesztekkel és GitHub Actions CI/CD pipeline-nal.

## Funkciók
- Feladat létrehozása, listázása, készre állítása, törlése
- `GET /health` állapotellenőrzés
- API: `GET /api/tasks`, `POST /api/tasks`, `PUT /api/tasks/{id}/complete`, `DELETE /api/tasks/{id}`
- Adatok memóriában tárolódnak, újraindításkor elvesznek (egyszerű demoalkalmazás)

## Helyi futtatás
Telepítsd a .NET 8 SDK-t, majd a projekt gyökerében:
```bash
dotnet run --project TaskManager.Api --urls http://localhost:8080
```
Böngésző: http://localhost:8080

## Tesztelés
```bash
dotnet test TaskManager.Tests/TaskManager.Tests.csproj
```

## Docker
```bash
docker build -t taskmanager .
docker run --rm -p 8080:8080 taskmanager
```

## GitHub feltöltés
1. Hozz létre egy üres GitHub repositoryt, például `cicd-taskmanager` néven.
2. Nyiss terminált a projekt gyökérmappájában:
```bash
git init
git branch -M main
git add .
git commit -m "Initial CI/CD project"
git remote add origin https://github.com/FELHASZNALONEV/cicd-taskmanager.git
git push -u origin main
```
3. GitHub -> Actions -> CI-CD: ellenőrizd a zöld futást.
4. A workflow buildel, xUnit teszteket futtat, publikálja az artifactot, Docker image-et készít, futtatja azt az Actions sandboxban, HTTP-n ellenőrzi a funkciókat, majd feltölti a Docker image-et a GHCR-be.

**Fontos:** Az Actions sandbox konténer csak a workflow futásáig él. A GHCR image tartós artifact, de nem nyilvános, állandóan futó alkalmazás. Ha az oktató tartósan elérhető deployt vár, külön hosting szükséges.

## Beadandó bizonyítékok
- GitHub repository link
- Actions sikeres workflow képernyőkép (build, test, deploy, HTTP ellenőrzés)
- Actions artifact: `taskmanager-publish`
- GHCR image csomag linkje
- Helyi böngészős képernyőkép a működő alkalmazásról

## Korlát
A projektet ebben a környezetben nem lehetett .NET SDK-val futtatni; a GitHub Actions első sikeres futása szolgál végső ellenőrzésként.
