

export class Recept {
        constructor( id , datumOd , datumDo, lekovi, klijent ) {
        this.miniKontejner = null;
        this.id = id;
        this.datumOd = datumOd;
        this.datumDo = datumDo;
        this.lekovi = lekovi;
        this.klijent = klijent;
    }
    
    crtajRecept(host) {
        this.miniKontejner = document.createElement("div");
        this.miniKontejner.classList.add("recept");

        
        let headerWrapper =  document.createElement("div");
        headerWrapper.className = "calendarWrapper";

        let odLbl = document.createElement("label");
        odLbl.className = "opis";
        odLbl.style.fontWeight ="700";
        odLbl.innerHTML = "Datum od";
        headerWrapper.appendChild(odLbl);

        let doLbl = document.createElement("label");
        doLbl.className = "opis";
        doLbl.style.fontWeight ="700";
        doLbl.innerHTML = "Datum do";
        headerWrapper.appendChild(doLbl);

        this.miniKontejner.appendChild(headerWrapper)


        
        const  formattedDateFrom = this.datumOd.toString().split("T")[0];
        const  formattedDateTo = this.datumDo.toString().split("T")[0];
        const  invalidDatumDo = +new Date(formattedDateTo) < +new Date();
        if(invalidDatumDo) this.miniKontejner.style.border = "4px solid red"; 

        let calendarWrapper =  document.createElement("div");
        calendarWrapper.className = "calendarWrapper";

        let datumOdSpan = document.createElement("span");
        datumOdSpan.className = "opis";
        datumOdSpan.innerHTML = formattedDateFrom;
        calendarWrapper.appendChild(datumOdSpan);

        let datumDoSpan = document.createElement("span");
        datumDoSpan.className = "opis";
        datumDoSpan.innerHTML = formattedDateTo;
        datumDoSpan.style.color = invalidDatumDo ? "red" : "inherit"
        calendarWrapper.appendChild(datumDoSpan);

        this.miniKontejner.appendChild(calendarWrapper);

        let lekoviWrapper = document.createElement("div");
        lekoviWrapper.className = "lekoviWrapper";
        
        this.lekovi.map(({ opis }) => {
            let lekSpan = document.createElement("span");
            lekSpan.className = "opis";
            lekSpan.style.fontStyle = "italic";
            lekSpan.innerHTML = opis.toUpperCase();
            lekoviWrapper.appendChild(lekSpan);
        })

        this.miniKontejner.appendChild(lekoviWrapper)


        let btnPronadji = document.createElement("button");
        btnPronadji.className="btnPotvrdi";
        btnPronadji.innerHTML= "Pronadji";
        this.miniKontejner.appendChild(btnPronadji);
      
        btnPronadji.onclick = () => {

        fetch("https://localhost:5001/Apoteka/PronadjiApoteku/" + this.id).then(p => {
            if(p.ok){
                p.json().then(data => { 
                    let pronadjenaApoteka = document.getElementsByClassName(`apoteka ${data.id}`)[0];
                    document.querySelector(`[class*="apoteka ${data.id}"]`)?.scrollIntoView({
                        behavior: "smooth",
                        block: "center",
                      }),
                    pronadjenaApoteka.style.border = "20px solid #00802b";
                    
                })}
                else{
                        p.json()
                        .then(er => alert(er.message ))
                       .catch(er => console.log(er));
        }})
        }


        let wrapperImg = document.createElement("div");
        wrapperImg.className= "wrapperImg";

        let closeImg = document.createElement("img");
        closeImg.src = "../close.jpg";
        closeImg.className = "obrisi";
        closeImg.alt = "obrisi";

        wrapperImg.appendChild(closeImg);
        this.miniKontejner.appendChild(wrapperImg);
        host.appendChild(this.miniKontejner);
  
        wrapperImg.onclick = () => {
        fetch("https://localhost:5001/Apoteka/IzbrisiRecept/" + this.id, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json"
            },}).then(p => {
                    if (p.ok) {
                        alert("Uspesno obrisan recept");
                     this.klijent.recepti.filter( ({ id }) => this.id !== id);
                     this.miniKontejner.classList.add("receptZaBrisanje");
                    
                     host.removeChild(document.querySelector(".receptZaBrisanje"))
                    }
                    else if (p.status == 400) {
                        console.log("greska")
                    }
                    else {
                        alert("Greška prilikom brisanja.");
                    }
                }).catch(p => { 
                    alert("Greska prilikom brisanja.")
                })
         }


         //edit

         let btnEditDatuma = document.createElement("button");
         btnEditDatuma.className = "btnPotvrdi"
         btnEditDatuma.innerHTML= "Promeni datum vazenja";
         this.miniKontejner.appendChild(btnEditDatuma);


         btnEditDatuma.onclick = () => {

            
            let modal = document.createElement("div");
            modal.className="modal";
            document.body.appendChild(modal);

            let editRecept = document.createElement("form");
            editRecept.className = "forma";
    
            let labelDo = document.createElement("label");
            labelDo.innerHTML = "Izmeni datum zavrsetka vazenja recepta:"

            let calendarDo = document.createElement("input");
            calendarDo.type = "date";
            calendarDo.className = "kalendar";

            let btnCancel = document.createElement("button");
            btnCancel.className="btnPotvrdi";
            btnCancel.style.backgroundColor = "black";
            btnCancel.innerHTML = "Zatvori";
            btnCancel.style.width = "100%";


    
            btnCancel.onclick = () => {
              document.body.removeChild(document.querySelector(".modal"))
            }
            
    
            let btnIzmeni = document.createElement("button");
            btnIzmeni.innerHTML = "Izmeni";
            btnIzmeni.type = "button",
            btnIzmeni.className = "btnPotvrdi";
            btnIzmeni.value="Izmeni";
            btnIzmeni.style.width = "100%";



            btnIzmeni.onclick = () => {
                const requestOptions = {
                method: 'PUT',
                headers: {   
                'Content-Type': 'application/json' 
                },
                body: JSON.stringify({id: this.id , datumDo: calendarDo.value}),
              
                };
                fetch("https://localhost:5001/Apoteka/IzmeniRecept", requestOptions)
                    .then(response => {
                        if(response.ok){
                            alert("Uspesno promenjen datuma vazenja");
                            datumDoSpan.innerHTML = calendarDo.value;
                           if(+new Date(calendarDo.value) < +new Date()) { 
                             this.miniKontejner.style.border = "4px solid red"
                             datumDoSpan.style.color = "red"; 
                           }
                            else{
                              this.miniKontejner.style.border = "1px solid #00802b"
                              datumDoSpan.style.color = "inherit";
                          }
                              document.body.removeChild(document.querySelector(".modal"));
                        }
                        else{
                            response.json()
                        .then(er => alert(er.message ?? "Unesite datum isteka vazenja"))
                       .catch(er => console.log(er));
                    }
                    });
                }
            editRecept.appendChild(labelDo);
            editRecept.appendChild(calendarDo);
            editRecept.appendChild(btnIzmeni);
            editRecept.appendChild(btnCancel);
            modal.appendChild(editRecept);
         }

    }
}