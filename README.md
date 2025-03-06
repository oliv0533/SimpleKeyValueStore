# SimpleKeyValueStore

Dette er mit svar på en opgave stillet til en jobsamtale. 

## Overvejelser til løsningen.

### Error handling
Det ville være smart at have lidt bedre error og validation på de forskellige endpoints. Der er pt ingen validering og der bliver kastet exceptions ved alle fejl. En bedre løsning ville implementere validering af inputs fra API og fejl vil blive returneret som valideringsfejl i stedet for exceptions. 

### Lag mellem key-value store og endpoints. 
Optimalt ville et lag mellem API og funktionaliteten til storage være optimalt. Min bedste løsning vil være MediatR med queries og commands, som stod for at abstrahere kompleksitet væk fra endpoints og udføre eventuelt ændret logik omkring hentning og gemning af værdier. 

Det kunne f.eks. være hvis der var et caching lag, som skulle opdateres ved slet og gem. Et abstraheringslag kan hjælpe til ikke at pollute endpoints til at gøre flere ting en højst nødvendigt. 

### Test
Det ville være fedt at implementere unit testing, integration testing og system testing, for at være sikker på at funktionaliteten er solid og performant. 
