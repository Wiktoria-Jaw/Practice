# Practice
!!! For record: If any informations in database match a real person personal data, its only a coincidence . I make up all those data in my head, taking a random names, surnames, adress and everything. !!!

This project will be tested on Opera GX, Im not planning on making test on other web browsers (it can change, depends on my apprenticeship mentor instructions). 

It will be later used in school to pass preparations for exam.

Its a public repository, feel free to use it and take inspirations from it, I dont mind.

!!!!! ------------------------- !!!!!
            FOR TESTS

FOR API:
1. In file `appsettings.json` change DefaultConnection="...;Database=[`your database`];.."
2. Migration doesnt migrate data, only database structure.

FOR REACT:
1. In file `App.jsx` you can change `const employeeId = ...` for any number you want. You won't get an error if employee with such ID doesn't exists. App will add work day and break data without problem, inserting your value as `employeeId`. 

It works that way bc it's first version of this app. Later I will add login so `employeeId` will be given from login and there won't be a moment where code recive ID without employee.

!!!!! ------------------------- !!!!!