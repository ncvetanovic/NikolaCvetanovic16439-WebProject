export class Klijent {
    constructor( id , email, username,password, recepti ) {
        this.id = id;
        this.email = email;
        this.password = password;
        this.username = username;
        this.lekovi = recepti;
    }
}