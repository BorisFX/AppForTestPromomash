import { Component, OnInit } from '@angular/core';
import {
  ReactiveFormsModule,
  FormGroup,
  Validators,
  FormBuilder,
} from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { User } from '../../models/user.model';

@Component({
  standalone: true,
  selector: 'app-user-registration',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.sass'],
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule],
  providers: [UserService],
})
export class UserRegistrationComponent implements OnInit {
  registrationForm!: FormGroup;

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.registrationForm = this.formBuilder.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: [
          '',
          [
            Validators.required,
            Validators.pattern('^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$'),
          ],
        ],
        confirmPassword: [''],
        agreement: ['', Validators.requiredTrue]
      },
      { validator: this.checkPasswords }
    );
  }

  checkPasswords(group: FormGroup): { [key: string]: any } | null {
    const passwordControl = group.get('password');
    const confirmPasswordControl = group.get('confirmPassword');

    if (passwordControl && confirmPasswordControl) {
      const password = passwordControl.value;
      const confirmPassword = confirmPasswordControl.value;
      return password === confirmPassword ? null : { notSame: true };
    }
    return null;
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      const user: User = {
        email: this.registrationForm.value.email,
        password: this.registrationForm.value.password,
      };

      this.userService.register(user).subscribe(
        (response) => {
          console.log('User registered successfully!', response);
          sessionStorage.setItem('userId', response.userId);
          alert(
            'User registered successfully! Please proceed to select your country.'
          );
          this.router.navigate(['/select-country']);
        },
        (error) => {
          console.error('Error registering user', error);
          alert('Error registering user. Please try again.');
        }
      );
    } else {
      alert('Please fill in all required fields.');
    }
  }
}
