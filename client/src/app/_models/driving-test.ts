import { DrivingSessionModel } from "./driving-session";

export interface DrivingTestModel {
        drivingSessionId:number;

        drivingSession: DrivingSessionModel;

        examinerUsername: string;

        passed: boolean;

        examinerRemarks: string;
}