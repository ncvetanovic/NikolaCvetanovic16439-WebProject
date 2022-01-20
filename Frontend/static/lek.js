export class Lek {
    constructor( id ,opis , cena ) {
        this.miniKontejner = null;
        this.id = id;
        this.opis = opis;
        this.cena = cena;
    }
    
    crtajLek(host) {
        this.miniKontejner = document.createElement("div");
        this.miniKontejner.className = "rowWrapper";

        let checkbox = document.createElement("input");
        checkbox.className = "checkboxLek"
        checkbox.type = "checkbox";
        checkbox.value = this.id;
        checkbox.style.backgroundColor = "#000000"

        let opisSpan = document.createElement("span");
        opisSpan.className = "opis";
        opisSpan.innerHTML = this.opis;

        let cenaSpan = document.createElement("span");
        cenaSpan.className = "opis";
        cenaSpan.innerHTML = this.cena;
        
        this.miniKontejner.appendChild(opisSpan);
        this.miniKontejner.appendChild(cenaSpan);
        this.miniKontejner.appendChild(checkbox);   


        host.appendChild(this.miniKontejner);

    }
}