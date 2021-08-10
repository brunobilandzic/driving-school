import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Pagination } from '../_models/pagination';
import { RegulationsTestModel } from '../_models/regulations-test';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class RegulationsTestsService {
  regulationsTests = new Map();
  baseUrl = environment.baseApiUrl;
  constructor(private http: HttpClient) { }

  loadRegulationsTests(pageNumber: number, pageSize: number){
    let key = pageNumber + '-' + pageSize;
    console.log(pageNumber, pageSize)
    if(this.regulationsTests.get(key)) return of(this.regulationsTests.get(key));

    let params = getPaginationHeaders(pageNumber, pageSize);

    return getPaginatedResult<RegulationsTestModel []>(this.baseUrl + 'professor/regulations-tests', params, this.http)
      .pipe(map((regTests) => {
        this.regulationsTests.set(key, regTests)
        return regTests;
      }))
  }

  deleteRegulationsTest(regulationsTestId: number) {
    return this.http.delete(this.baseUrl + 'professor/regulations-tests/' + regulationsTestId, {})
      .pipe(map(() => {
        this.regulationsTests = new Map();
      }));
  }


  addRegulationsTest(regulationsTest: any) {
    console.log(regulationsTest);
    
    return this.http.post(this.baseUrl + 'professor/regulations-tests/',  regulationsTest);
  }

  editRegulationsTest(regulationsTest: any, regulationsTestId: number){
    console.log(regulationsTest, regulationsTestId);

    return this.http.put(this.baseUrl + 'professor/regulations-tests/' + regulationsTestId, regulationsTest);
  }

}
