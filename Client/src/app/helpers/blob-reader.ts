import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class BlobReader {
  public readAsString(blob: any | string): Observable<string> {
    return new Observable<string>((observer: any) => {
      if (!blob) {
        observer.next("");
        observer.complete();
        return;
      }

      if (blob instanceof Blob) {
        const reader = new FileReader();
        reader.onload = function () {
          observer.next(this.result);
          observer.complete();
        };
        reader.readAsText(blob);
        return;
      }

      observer.next(blob);
      observer.complete();
    });
  }
}
