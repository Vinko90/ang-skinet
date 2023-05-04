import { Component } from '@angular/core';
import {AbstractControl, AsyncValidatorFn, FormBuilder, Validators} from "@angular/forms";
import {AccountService} from "../account.service";
import {Router} from "@angular/router";
import {debounceTime, finalize, map, switchMap, take} from "rxjs";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  registrationErrors: string[] | null = null;

  passwordRegex = "(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$";

  registerForm = this.fb.group({
    displayName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email], [this.validateEmailNotTaken()]],
    password: ['', [Validators.required, Validators.pattern(this.passwordRegex)]]
  });

  constructor(private fb: FormBuilder, private accSrv: AccountService, private router: Router) { }

  onSubmit() {
    this.accSrv.register(this.registerForm.value).subscribe({
      next: () => this.router.navigateByUrl('/shop'),
      error: err => this.registrationErrors = err.errors
    });
  }

  //Debounce functionality for async validation to API
  validateEmailNotTaken(): AsyncValidatorFn {
    return (control: AbstractControl) => {
      return control.valueChanges.pipe(
        debounceTime(1000),
        take(1),
        switchMap(() => {
          return this.accSrv.checkEmailExists(control.value).pipe(
            map(result => result ? {emailExists: true} : null),
            finalize(() => control.markAsTouched())
          )
        })
      )
    };
  }
}
