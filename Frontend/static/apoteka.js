export class Apoteka {
    constructor(id,naziv,adresa,opis) {
        this.miniKontejner = null;
        this.id=id;
        this.naziv=naziv;
        this.adresa=adresa;
        this.opis=opis;
    }
    
    crtajApoteku(host) {
        this.miniKontejner = document.createElement("div");
        this.miniKontejner.className = "apoteka";

        let nazivLabela = document.createElement("h1");
        nazivLabela.innerHTML = this.naziv;
        this.miniKontejner.appendChild(nazivLabela);

        let opisSpan = document.createElement("span");
        opisSpan.className = "opis";
        opisSpan.innerHTML = this.opis;
        this.miniKontejner.appendChild(opisSpan);

        let kreirajButton = document.createElement("button");
        kreirajButton.innerHTML  = "Kreiraj novu apoteku";
        this.miniKontejner.appendChild(kreirajButton);

        kreirajButton.onclick = () => {

            fetch("https://localhost:5001/Apoteka/KreirajApoteku", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                 },
                body: JSON.stringify({
                naziv: "betmene",
                opis: "happy birthday",
                adresa: "Lalinac",
                lekovi: null,
                recepti: null,
            })
                }).then(p => {
                    // if (p.ok) {
                    // console.log("alal ti")
                    // }
                    // else if (p.status == 400) {
                    // console.log("greska")
                    // }
                    // else {
                    //     alert("Greška prilikom kreiranja.");
                    // }
                    p.json().then(data => console.log(data))
                }).catch(p => {
                    alert("Greška prilikom kreiranja.");
                })

        };

        host.appendChild(this.miniKontejner);

    }
}