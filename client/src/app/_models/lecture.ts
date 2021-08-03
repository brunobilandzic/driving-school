import { LectureTopic } from "./lecture-topic";
import { UserModel } from "./user";

export interface Lecture {
    lectureId: number;
    regulationsGroupId: number;
    professor: UserModel;
    professorRemark?: string;
    dateStart: Date;
    lectureTopic: LectureTopic;
}