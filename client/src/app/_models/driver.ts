import { DrivingSessionModel } from "./driving-session";
import { UserModel } from "./user";

export interface DriverModel extends UserModel {
    drivingSessionsTaken: DrivingSessionModel [];
}