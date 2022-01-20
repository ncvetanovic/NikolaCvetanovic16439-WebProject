export class Apoteka {
    constructor( id , naziv , adresa , opis, lekovi ) {
        this.miniKontejner = null;
        this.id = id;
        this.naziv = naziv;
        this.adresa = adresa;
        this.opis = opis;
        this.lekovi = lekovi;
    }
    
    crtajApoteku(host) {

        this.miniKontejner = document.createElement("div");
        this.miniKontejner.classList.add("apoteka");
        this.miniKontejner.classList.add(this.id);

        let nazivLabela = document.createElement("h1");
        nazivLabela.innerHTML = this.naziv.toUpperCase();
        nazivLabela.style.fontSize = "30px";
        this.miniKontejner.appendChild(nazivLabela);

        let lblLokacija = document.createElement("label");
        lblLokacija.innerHTML = "Lokacija"
        lblLokacija.style.fontWeight = "600";
        this.miniKontejner.appendChild(lblLokacija)

        let opisSpan = document.createElement("span");
        opisSpan.className = "opis";
        opisSpan.innerHTML = this.opis;
        opisSpan.style.fontWeight = "500";
        this.miniKontejner.appendChild(opisSpan);

        host.appendChild(this.miniKontejner);

    }
}