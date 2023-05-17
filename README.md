# Specifikáció

## Feladat

A feladat egy olyan webalkalmazás készítése, ahol a felhasználók létre tudnak hozni esport versenyeket, vezethetik az eredményeket, megszervezhetik a jelentkeztetést. A felhasználók ezen kívül létre tudnak hozni csapatokat, amikkel tudnak jelentkezni ezekre a versenyekre. A csapatok ezen kívül tudnak publikusan tagfelvételt hirdetni.

## Követelmények

- A felhasználó tudjon regisztrálni, belépni
- A felhasználó regisztrálásnál megad egy egyedi felhasználónevet, amit később tud módosítani
- A felhasználó tudjon létrehozni versenyeket, ekkor ő lesz a verseny szervezője
- A versenyek létrehozásánál a szervező beállíthatja, hogy melyik játékot/esportot játszák
- A szervezőnek be lehet állítani a verseny formátumát (körmérkőzéses, egyenes kieséses, vigaszágas kiesés)
- A szervezőnek lehet az egyes meccsek eredményét vezetni
- A szervező megadhatja, hogy hány csapat indullhat
- A szervező elfogadhatja, illetve elutasíthatja a csapatok jelentkezését
- A szervező a jelentkezés lezárta után megadhatja a csapatok kiemelését (vagyis feltételezett erősség szerint sorba rakja a csapatokat) és ez alapján lesz legenerálva az ágrajz
- A versenyekre lehet publikusan keresni név szerint, illetve szűrni játékra/esportra, szervezőre, illetve státuszra (lehet jelentkezni rá/nem lehet/elindult a verseny)
- A felhasználó tudjon csapatot létrehozni, ekkor ő lesz a csapatkapitánya
- A csapatkapitány a csapat létrehozásánál megad egy egyedi nevet, amit később megváltoztathat
- A csapatkapitány beállíthatja, hogy melyik játékban/esportban versenyeznek
- A csapatkapitány tud versenyekre jelentkezni a csapattal
- A csapatkapitány tud tagfelvételt hírdetni, írhat oda rövid leírást, hogy mit várnak
- A csapatkapitány elfogadhatja vagy elutasíthatja a tagfelvételre jelentkezett játékosokat
- A csapatkapitány meg tud hívni specifikus embert a csapatba felhasználónév szerint
- A csapatok publikusak, rájuk lehet keresni név szerint, illetve szűrni játék/esport szerint, és tagfelvétel szerint is


## Technológiák

- Adatbázis: MSSQL
- Backend: ASP.NET Core, Entitiy Framework, REST API
- Frontend: Angular
- Verziókezelés: GitHub