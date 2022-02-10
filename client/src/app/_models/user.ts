export interface User {
    //Interfata in TypeScript nu este o interfata normala din OOP. Cand ne referim la Interface in TypeSript, ne referim la
    //Faptul ca ceva este un anumit ceva

    userName?: string;
    token?: string;
    photoUrl?: string;
    knownAs?: string;
    gender?: string;
    roles?: string[];
}