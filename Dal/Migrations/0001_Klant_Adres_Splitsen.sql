-- Migratie: Klant.Adres splitsen in Straatnaam en Huisnummer
-- Voer dit script éénmalig uit in SQL Server Management Studio / Azure Data Studio.

-- Stap 1: nieuwe kolommen toevoegen als nullable
ALTER TABLE Klant ADD Straatnaam NVARCHAR(200) NULL;
ALTER TABLE Klant ADD Huisnummer NVARCHAR(10)  NULL;

-- Stap 2: bestaande data migreren
--   Formaat wordt aangenomen als "Straatnaam Huisnummer" (spatie als scheidingsteken).
--   Alles vóór de eerste spatie wordt Straatnaam, de rest Huisnummer.
--   Heeft een rij geen spatie, dan gaat de gehele waarde naar Straatnaam en Huisnummer wordt ''.
UPDATE Klant
SET
    Straatnaam = CASE
        WHEN CHARINDEX(' ', Adres) > 0
            THEN LEFT(Adres, CHARINDEX(' ', Adres) - 1)
        ELSE Adres
    END,
    Huisnummer = CASE
        WHEN CHARINDEX(' ', Adres) > 0
            THEN LTRIM(SUBSTRING(Adres, CHARINDEX(' ', Adres) + 1, LEN(Adres)))
        ELSE ''
    END;

-- Stap 3: kolommen verplicht maken (nadat alle rijen zijn gevuld)
ALTER TABLE Klant ALTER COLUMN Straatnaam NVARCHAR(200) NOT NULL;
ALTER TABLE Klant ALTER COLUMN Huisnummer NVARCHAR(10)  NOT NULL;

-- Stap 4: oude kolom verwijderen
ALTER TABLE Klant DROP COLUMN Adres;
