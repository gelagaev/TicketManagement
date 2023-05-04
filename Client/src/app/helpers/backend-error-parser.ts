import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { BlobReader } from "./blob-reader";
import { BackendError } from "../interceptors/http-request-failure.interceptor";

@Injectable({
  providedIn: "root",
})
export class BackendErrorParser {
  constructor(private blobReader: BlobReader) {
  }

  public parseErrorResponse(httpErrorResponse: HttpErrorResponse): Observable<BackendError> {
    return this.blobReader.readAsString(httpErrorResponse.error)
      .pipe(map(content => {
        return JSON.parse(content) as BackendError;
      }));
  }
}
