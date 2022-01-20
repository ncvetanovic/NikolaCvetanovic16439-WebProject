export const crtajModal = () => {
    let modal = document.createElement("div");
    modal.className="modal";
    document.body.appendChild(modal);
    return modal;
}

export const crtajBtnCancel = () => {
    let btnCancel = document.createElement("button");
    btnCancel.className="btnPotvrdi";
    btnCancel.style.backgroundColor = "black";
    btnCancel.innerHTML = "Zatvori";
    btnCancel.type = "button";

    btnCancel.onclick = () => {
      document.body.removeChild(document.querySelector(".modal"))
    }
}