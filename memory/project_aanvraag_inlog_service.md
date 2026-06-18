---
name: IAanvraagService en IInlogService bewaren
description: IAanvraagService en IInlogService blijven voorlopig als interface bestaan — worden in een later stadium aangepast
type: project
---

`IAanvraagService` en `IInlogService` (beide in `Interface/Services/`) blijven voor nu staan als interface.

**Why:** Ze worden in een later stadium op dezelfde manier refactored als `IAbonnementService` — YAGNI, concrete service direct injecteren. Nog niet aan toe.

**How to apply:** Niet verwijderen of aanpassen totdat de gebruiker het overzicht voor die entiteiten aanlevert. Net als bij Abonnement: stap voor stap, laag voor laag, wachten op akkoord.
