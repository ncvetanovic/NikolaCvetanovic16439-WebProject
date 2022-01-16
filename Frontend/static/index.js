

// fetch("https://localhost:5001/Apoteka/KreirajLek", {
//     method: "POST",
//     headers: {
//         "Content-Type": "application/json"
//     },
//     body: JSON.stringify({
//        opis: "test sssss",
//        cena: 43,
//     })
// }).then(p => {
//     if (p.ok) {
//        console.log("alal ti")
//     }
//     else if (p.status == 400) {
//        console.log("greska")
//     }
//     else {
//         alert("Greška prilikom kreiranja.");
//     }
// }).catch(p => {
//     alert("Greška prilikom kreiranja.");
// })

import { Apoteka } from "./apoteka.js";


// import { Apoteka } from "./Apoteka";





// fetch("https://localhost:5001/Apoteka/IzbrisiLek/" + 12, {
//     method: "DELETE",
//     headers: {
//            "Content-Type": "application/json"

//     },
 
//         }).then(p => {
//             if (p.ok) {
//                console.log("alal ti")
//             }
//             else if (p.status == 400) {
//                console.log("greska")
//             }
//             else {
//                 alert("Greška prilikom kreiranja.");
//             }
//         }).catch(p => {
//             alert("Greška prilikom kreiranja.");
// })




fetch("https://localhost:5001/Apoteka/PreuzmiSveLekove").then(p => {
        p.json().then(data => {
          console.log(data)
        });
});


fetch("https://localhost:5001/Apoteka/PreuzmiSveApoteke").then(p => {
        p.json().then(data => {
          console.log(data)
      
          const container = document.createElement("div");
          container.className = "apoteka-container";
          document.body.appendChild(container);
        data.map((apoteka) => {
            const apotekaObject = new Apoteka(apoteka.id,apoteka.naziv,apoteka.adresa,apoteka.opis);
            apotekaObject.crtajApoteku(container);    
            console.log(apoteka)
        })

        data.map((apoteka) => {
          const apotekaObject = new Apoteka(apoteka.id,apoteka.naziv,apoteka.adresa,apoteka.opis);
          apotekaObject.crtajApoteku(container);    
          console.log(apoteka)
      })
     
        });
});
