import { FormControl } from "@angular/forms";
import { ErrorStateMatcher } from "@angular/material/core";

//TODO this is fix fo known issue with form submitting https://github.com/angular/components/issues/4190
export class CustomMaterialFormsMatcher implements ErrorStateMatcher {
    isErrorState(control: FormControl | null): boolean {
        return !!(control && control.invalid && (control.dirty || control.touched));
    }
}
