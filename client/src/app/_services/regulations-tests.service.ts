import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IdToId } from '../_models/id-to-id';
import { PaginatedResult, Pagination } from '../_models/pagination';
import { RegulationsTestModel } from '../_models/regulations-test';
import { StudentScore } from '../_models/student-score';
import { UsernamesToId } from '../_models/usernames-to-id';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class RegulationsTestsService {
  regulationsTests = new Map();
  updateRegulationsTest = new BehaviorSubject<RegulationsTestModel>(null);
  updateRegulationsTest$ = this.updateRegulationsTest.asObservable();
  baseUrl = environment.baseApiUrl;
  constructor(private http: HttpClient) {}

  loadRegulationsTests(pageNumber: number, pageSize: number) {
    let key = pageNumber + '-' + pageSize;
    console.log(pageNumber, pageSize);
    if (this.regulationsTests.get(key))
      return of(this.regulationsTests.get(key));

    let params = getPaginationHeaders(pageNumber, pageSize);

    return getPaginatedResult<RegulationsTestModel[]>(
      this.baseUrl + 'professor/regulations-tests',
      params,
      this.http
    ).pipe(
      map((regTests) => {
        this.regulationsTests.set(key, regTests);
        return regTests;
      })
    );
  }

  addGroupToRegulationsTest(groupId: number, regulationsTestId: number) {
    let groupToTest = new IdToId();
    groupToTest.idFrom = groupId;
    groupToTest.idTo = regulationsTestId;

    return this.http.post(
      this.baseUrl + 'professor/group-to-test',
      groupToTest
    );
  }

  fetchRegulationsTest(regulationsTestId: number) {
    return this.http.get(
      this.baseUrl + 'professor/regulations-tests/' + regulationsTestId
    );
  }

  deleteRegulationsTest(regulationsTestId: number) {
    return this.http
      .delete(
        this.baseUrl + 'professor/regulations-tests/' + regulationsTestId,
        {}
      )
      .pipe(
        map(() => {
          this.regulationsTests = new Map();
        })
      );
  }

  updateTests(pageNumber: number, pageSize: number) {
    this.regulationsTests = new Map();
    return this.loadRegulationsTests(pageNumber, pageSize);
  }

  deleteStudentsFromRegulationsTest(
    usernames: string[],
    regulationsTestId: number
  ) {
    let usernamesToId = new UsernamesToId();
    usernamesToId.usernames = usernames;
    usernamesToId.id = regulationsTestId;

    return this.http.delete(
      this.baseUrl + 'professor/regulations-test-student-bunch',
      { body: usernamesToId }
    );
  }

  addRegulationsTest(regulationsTest: any) {
    console.log(regulationsTest);

    return this.http.post(
      this.baseUrl + 'professor/regulations-tests/',
      regulationsTest
    );
  }

  editRegulationsTest(regulationsTest: any, regulationsTestId: number) {
    console.log(regulationsTest, regulationsTestId);

    return this.http.put(
      this.baseUrl + 'professor/regulations-tests/' + regulationsTestId,
      regulationsTest
    );
  }

  examineRegulationsTest(
    regulationsTestId: number,
    studentScores: StudentScore[]
  ) {
    return this.http.post(
      this.baseUrl + 'professor/examine-test/' + regulationsTestId,
      studentScores
    );
  }
}
