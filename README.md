# ContactCatalog  
## Hur kör man programmet?

- När konsollen startas visas ett välkomstmeddelande och du får välja ett menyval (1-6). (felmeddelande om du skriver något annat än siffrorna 1-6)
### Menyval:
- Vid val av alternativ "1" kommer du att få uppge id, namn, epost och taggar för att lägga till en ny kontakt. Id, namn och epost är obligatoriska fält så om du inte skriver in något kommer ett felmeddelande.
  Skriver du in en mailadress som redan finns kommer också ett felmeddelande.
  
- Vid val av alternativ "2" kommer du få se en lista av alla kontakter i listan.
  
- Vid val av alternativ "3" kommer du få ange vad du vill söka efter, sökresultaten baseras på namnen i dem befintliga kontakterna och kommer visas i en lista.
  
- Vid val av alternativ "4" kommer du få ange vad du vill söka efter, sökresultaten baseras på taggarna som finns i dem befintliga kontakterna och kommer visas i en lista.
  
- Vid val av alternativ "5" kommer en CSV fil att exporteras.
  
- Vid val av alternativ "6" avslutas programmet

- Efter varje val får du trycka på valfri tangent för att återgå till menyn. Du kan även stänga konsollen manuellt.
  
- Tryck sedan på valfri tangent eller krysset för att stänga ner konsollen.

### Designval
- Tydlig struktur och bra översikt har skapats genom att dela upp programmet i flera mappar och klasser för att följa principen Separation of Concerns.
- LINQ används för att filtrera och sortera kontakter.
- Validering sker vid inmatning för att undvika fel (t.ex. e-post, telefonnummer).
- Felhantering: Try-catch-block och tydliga felmeddelanden för att undvika krascher.
- Kommenterad kod: Förklaringar i koden för att underlätta förståelse och samarbete.
- Språkval: Kod på engelska, användargränssnitt på svenska.

<img width="1797" height="783" alt="image" src="https://github.com/user-attachments/assets/64580dff-7281-402c-bb49-3b1c8de5c1d2" />
