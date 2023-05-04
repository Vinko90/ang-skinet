import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AccountService} from "../account.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  })

  constructor(private accService: AccountService, private router: Router) { }
  onSubmit() {
    this.accService.login(this.loginForm.value).subscribe({
      next: () => this.router.navigateByUrl('/shop')
    });
  }
}
