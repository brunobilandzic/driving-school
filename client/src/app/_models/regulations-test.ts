import { StudentTestModel } from "./student-test";
import { UserModel } from "./user";

export interface RegulationsTestModel {
    regulationsTestId: number;
    dateStart: Date;
    location?: string;
    examinerId: number;
    examiner: UserModel;
    studentRegulationsTest: StudentTestModel [];
}