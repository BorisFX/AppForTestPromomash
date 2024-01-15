import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { DataService } from '../services/data-service.service';
import { Country } from '../../models/country.model';
import { Province } from '../../models/province.model';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { UserService } from '../services/user.service';
import { SelectUserCountry } from '../../models/selectUserCountry.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-select-user-country',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './select-user-country.component.html',
  styleUrl: './select-user-country.component.sass',
  providers: [DataService, UserService],
})
export class SelectUserCountryComponent implements OnInit {
  detailsForm!: FormGroup;
  countries: Country[] = [];
  provinces: Province[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private dataService: DataService,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.detailsForm = this.formBuilder.group({
      country: ['', Validators.required],
      province: ['', Validators.required],
    });

    this.loadCountries();
  }
  loadCountries() {
    this.dataService.getCountries().subscribe(
      (countries: Country[]) => (this.countries = countries),
      (error) => console.error('Error loading countries', error)
    );
  }

  onCountryChange() {
    const selectedCountry = this.detailsForm.value.country;
    this.provinces = [];
    this.detailsForm.get('province')?.reset();

    if (selectedCountry) {
      this.dataService.getProvinces(selectedCountry).subscribe(
        (provinces: Province[]) => (this.provinces = provinces),
        (error) => console.error('Error loading provinces', error)
      );
    }
  }

  onSave() {
    if (this.detailsForm.valid) {
      const userId = sessionStorage.getItem('userId');
      if (!userId) {
        alert('User ID is missing. Please complete the registration process.');

        this.router.navigate(['/register']);
        return;
      }

      const command: SelectUserCountry = {
        userId: userId,
        countryId: this.detailsForm.value.country,
        provinceId: this.detailsForm.value.province,
      };
      this.userService.selectUserCountry(command).subscribe(
        (response) => {
          console.log('Country and province selected successfully', response);
          sessionStorage.clear();
          alert(
            'Country and province selection saved successfully. Please login again.'
          );
          this.router.navigate(['/register']);
        },
        (error) => {
          console.error('Error during country and province selection', error);
          alert('There was an error saving your selection. Please try again.');
        }
      );
    } else {
      alert('Please fill in all required fields.');
    }
  }
}
