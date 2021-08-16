import { Pagination } from "./pagination";

export interface SessionParams extends Pagination {
    dateStart: Date;
    isDriven: boolean;
}