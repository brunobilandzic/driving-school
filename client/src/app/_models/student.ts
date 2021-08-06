import { Lecture } from "./lecture";
import { RegulationsTestModel } from "./regulations-test";
import { StudentLectureModel } from "./student-lecture";
import { StudentTestModel } from "./student-test";
import { UserModel } from "./user";

export interface StudentModel extends UserModel {
    regulationsTests: StudentTestModel [];
    lectures: StudentLectureModel[];
    regulationsGroupId: number;
    passed: boolean;
}