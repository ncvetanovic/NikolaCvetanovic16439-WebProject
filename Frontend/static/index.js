
import { Apoteka } from "./apoteka.js";
import { crtajModal } from "./helpers.js";
import { Klijent } from "./klijent.js";
import { Lek } from "./lek.js";
import { Recept } from "./recept.js"; 

let loginContainer = document.createElement("div");
loginContainer.className = "loginContainer";

let btnLogovanje = document.createElement("button");
btnLogovanje.className = "btnLogovanje";
btnLogovanje.innerHTML="Uloguj se";
btnLogovanje.type="button";
loginContainer.appendChild(btnLogovanje);

btnLogovanje.onclick = () => {
    let modal = document.createElement("div");
    modal.className="modal";
    document.body.appendChild(modal)
    crtajLogin(modal);
}

let btnRegister = document.createElement("button");
btnRegister.innerHTML="Registruj se";
btnRegister.className = "btnRegistracija";
btnRegister.type="button";
btnRegister.style.backgroundColor ="#045487";
loginContainer.appendChild(btnRegister);

btnRegister.onclick = () => {
    let modal = document.createElement("div");
    modal.className="modal";
    document.body.appendChild(modal)
    crtajRegister(modal);
}


document.body.appendChild(loginContainer);


fetch("https://localhost:5001/Apoteka/PreuzmiSveApoteke").then(p => {
        p.json().then(data => {
          
          const container = document.createElement("div");
          container.className = "apoteka-container";
          main.appendChild(container);
         
          data.map(( { id , naziv, adresa, opis, lekovi} ) => {
            const apotekaObject = new Apoteka( id, naziv, adresa, opis, lekovi);
            apotekaObject.crtajApoteku(container);    
      })  
    });
});


const main = document.createElement("div");
main.className = "main";
document.body.appendChild(main);

let klijent = new Klijent();
let globalLekovi = [];



  const  crtajLogin = (host) =>{
        let loginForma = document.createElement("form");
        loginForma.className = "forma";

        let lblUser= document.createElement("label");
        lblUser.innerHTML="Korisnicko ime:";
        let inUser = document.createElement("input");

        loginForma.appendChild(lblUser);
        loginForma.appendChild(inUser);

        let lblPass= document.createElement("label");
        lblPass.innerHTML="Lozinka:";
        let inPass = document.createElement("input");
        inPass.className="user";
        inPass.type="password";
    
        loginForma.appendChild(lblPass);
        loginForma.appendChild(inPass);

        let btnLogin = document.createElement("button");
        btnLogin.innerHTML="Uloguj se";
        btnLogin.className="btnPotvrdi";
        btnLogin.type = "submit";
        btnLogin.value="Uloguj se";
        
        loginForma.appendChild(btnLogin);

        loginForma.onsubmit = () => login(inUser.value , inPass.value);

        loginForma.addEventListener("submit", function(evt) {
          evt.preventDefault();
        });
        
        let btnCancel = document.createElement("button");
        btnCancel.className="btnPotvrdi";
        btnCancel.style.backgroundColor = "black";
        btnCancel.innerHTML = "Zatvori";
        btnCancel.type = "button";

        btnCancel.onclick = () => {
          document.body.removeChild(document.querySelector(".modal"))
        }
        loginForma.appendChild(btnCancel)
        host.appendChild(loginForma);
}

const  crtajRegister = (host) =>{
      let registerForma = document.createElement("form");
      registerForma.className = "forma";
    
      let lblUser = document.createElement("label");
      lblUser.innerHTML="Korisnicko ime:";
      let inUser = document.createElement("input");

      registerForma.appendChild(lblUser);
      registerForma.appendChild(inUser);

      let lblPass= document.createElement("label");
      lblPass.innerHTML="Lozinka:";
      let inPass = document.createElement("input");
      inPass.type="password";

      let lblEmail= document.createElement("label");
      lblEmail.innerHTML="Email adresa:";
      let inEmail = document.createElement("input");
      inEmail.type="email";
      inEmail.patter=".+@globex\.com";
      inEmail.required;
  
      registerForma.appendChild(lblPass);
      registerForma.appendChild(inPass);
      registerForma.appendChild(lblEmail);
      registerForma.appendChild(inEmail);

      let btnLogin = document.createElement("button");
      btnLogin.innerHTML="Registruj se";
      btnLogin.className="btnPotvrdi";
      btnLogin.type = "submit";
      
      registerForma.appendChild(btnLogin);

      registerForma.onsubmit = () => register(inUser.value , inPass.value, inEmail.value);

      registerForma.addEventListener("submit", function(evt) {
        evt.preventDefault();
      });
      
      let btnCancel = document.createElement("button");
      btnCancel.className="btnPotvrdi";
      btnCancel.style.backgroundColor = "black";
      btnCancel.innerHTML = "Zatvori";
      btnCancel.type = "button";

      btnCancel.onclick = () => {
        document.body.removeChild(document.querySelector(".modal"))
      }
      registerForma.appendChild(btnCancel)
      host.appendChild(registerForma);
}
const register = (username,password,email) => {
  const requestOptions = {
    method: 'POST',
    headers: {   
    'Content-Type': 'application/json' 
    },
    body: JSON.stringify({username, password, email }),
  
    };
    fetch("https://localhost:5001/Apoteka/KreirajKlijenta", requestOptions)
        .then(response => {
            if(response.ok){
                alert("Uspesno ste kreirali nalog, dobrodosli!");
                document.body.removeChild(document.querySelector(".modal"));
                document.body.removeChild(document.querySelector(".loginContainer"));
                response.json().then(user => preuzmiKlijenta(user.id));
            }
            else{
              response.json()
               .then(er => alert(er.message))
               .catch(er => console.log(er));
            }
  });  
}

const login =  ( username , password) => {
  const requestOptions = {
    method: 'POST',
    headers: {   
    'Content-Type': 'application/json' 
    },
    body: JSON.stringify({username, password }),
  
    };
    fetch("https://localhost:5001/Apoteka/Login", requestOptions)
        .then(response => {
            if(response.ok){
                alert("Uspesno ste se ulogovali");
                document.body.removeChild(document.querySelector(".modal"));
                document.body.removeChild(document.querySelector(".loginContainer"));
                response.json().then(user => preuzmiKlijenta(user.id) );
            }
            else{
              response.json()
               .then(er => alert(er.message))
              .catch(er => console.log(er));

            }
  });  
}


const preuzmiKlijenta = (idKlijenta) => {
fetch("https://localhost:5001/Apoteka/PreuzmiKlijenta/" + idKlijenta).then(p => p.json().then(data => {

  klijent = data;
  const container = document.createElement("div");
  container.className = "recept-container";

  let btnKreirajRecept = document.createElement("button");
  btnKreirajRecept.type="button";
  btnKreirajRecept.className ="btnPotvrdi";
  btnKreirajRecept.style.marginLeft = "24px";
  btnKreirajRecept.style.width = "250px";
  btnKreirajRecept.innerHTML="Dodaj recept";

  main.appendChild(container);
  main.appendChild(btnKreirajRecept);
  main.style.flexDirection = "column-reverse";
  
  data.recepti.map(({ id, datumOd, datumDo, lekovi}) => {
    const receptObjekat = new Recept( id, datumOd, datumDo, lekovi, klijent);
    receptObjekat.crtajRecept(container);    
  }) 

  btnKreirajRecept.onclick = () => {
        let modal =  crtajModal();
        fetch("https://localhost:5001/Apoteka/PreuzmiSveLekove").then(p => {
        
        if(p.ok){
        p.json().then(response => {
        globalLekovi = response;
        
        let dodajReceptFroma = document.createElement("form");
        dodajReceptFroma.className = "forma";
        dodajReceptFroma.onsubmit = kreirajRecept;  

        let labelOd = document.createElement("label");
        labelOd.innerHTML = "Datum vazenja recepta od:"

        let calendarOd = document.createElement("input");
        calendarOd.type = "date";
        calendarOd.className = "kalendar"
        
        let labelDo = document.createElement("label");
        labelDo.innerHTML = "Datum vazenja recepta do:"

        let calendarDo = document.createElement("input");
        calendarDo.type = "date";
        calendarDo.className = "kalendar";
   

        dodajReceptFroma.appendChild(labelOd);
        dodajReceptFroma.appendChild(calendarOd);
        dodajReceptFroma.appendChild(labelDo);
        dodajReceptFroma.appendChild(calendarDo);



        let header = document.createElement("div");
        header.className = "rowWrapper";


        let nazivLbl = document.createElement("label");
        nazivLbl.className = "opis";
        nazivLbl.innerHTML = "Naziv";
        nazivLbl.style.color = "#00802b";
        header.appendChild(nazivLbl)

        let cenaLbl = document.createElement("label");
        cenaLbl.className = "opis";
        cenaLbl.innerHTML = "Cena";
        cenaLbl.style.color = "#00802b";

        header.appendChild(cenaLbl)

        dodajReceptFroma.appendChild(header);

        
        dodajReceptFroma.addEventListener("submit", function(ev) {
          ev.preventDefault();
        });

        response.map(({ id , opis, cena}) => {
          const lek = new Lek(id, opis, cena);
          lek.crtajLek(dodajReceptFroma);
        })
        let btnWrapper = document.createElement("div");
        btnWrapper.className = "rowWrapper";

        let btnCancel = document.createElement("button");
        btnCancel.className="btnPotvrdi";
        btnCancel.style.backgroundColor = "black";
        btnCancel.style.width = "200px";
        btnCancel.innerHTML = "Zatvori";
        btnCancel.type = "button";

        btnCancel.onclick = () => {
          document.body.removeChild(document.querySelector(".modal"))
        }
        

        let btnPotvrdi = document.createElement("input");
        btnPotvrdi.innerHTML = "Potvrdi";
        btnPotvrdi.className = "btnPotvrdi";
        btnPotvrdi.style.width = "200px";
        btnPotvrdi.type = "submit";
        btnPotvrdi.value="Potvrdi";

        btnWrapper.appendChild(btnCancel);
        btnWrapper.appendChild(btnPotvrdi);
     
        dodajReceptFroma.appendChild(btnWrapper)

        modal.appendChild(dodajReceptFroma);
      
        
        });
      }
      else{
        response.json()
        .then(er => alert(er.message))
       .catch(er => console.log(er));

      }
    }
    );
    }
  })
  )
}

const kreirajRecept = () => {
  
  const checkboxoviLek = document.querySelectorAll(".checkboxLek");
  const datumVazenja = document.querySelectorAll(".kalendar");
  const lekoviAPI = [];

  checkboxoviLek.forEach((lekCheckbox) => {
    if(lekCheckbox.checked){
      globalLekovi.map(( {id , opis, cena}) => {
        if(id.toString() === lekCheckbox.value)
          lekoviAPI.push({ id, opis , cena});
      });
    }
  });

  fetch("https://localhost:5001/Apoteka/KreirajRecept/" + klijent.id, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
          datumOd: datumVazenja[0].value,
          datumDo: datumVazenja[1].value,
          lekovi: lekoviAPI
        })
      })
      .then(p => {  
        if(p.ok){
        p.json()
        .then(response => {    
          alert("Uspesno kreiran recept!")      
          klijent.recepti.push(response);
          let container = document.querySelector(".recept-container");
          const noviRecept = new Recept(response.id, response.datumOd, response.datumDo, response.lekovi, klijent);
          document.body.removeChild(document.querySelector(".modal"))
          noviRecept.crtajRecept(container);  
        })}else{
          p.json()
          .then(er => alert(er.message ?? "Unesite datum"))
         .catch(er => console.log(er));
        }
      });
}




