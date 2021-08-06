export interface UserModel {
    username: string;
    firstName: string;
    lastName: string;
    photoUrl: string;
    token: string;
    roles: string [];
    dateRegistered: Date;
}