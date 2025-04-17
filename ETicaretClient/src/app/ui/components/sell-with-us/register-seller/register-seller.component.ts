import { Component, OnInit, OnDestroy, ElementRef, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MenuItem } from 'primeng/api';
import { SupplierRegistrationService } from '../../../../services/common/models/supplier-registration.service';
import { CreateSupplierRegistration } from '../../../../contracts/supplierRegistrationRequest/createSupplierRegistration';
import { Router, ActivatedRoute } from '@angular/router';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Subscription } from 'rxjs';
import { AuthInterceptorService } from '../../../../interceptors/auth-interceptor.service';
import { SupplierService } from '../../../../services/common/models/supplier.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';

@Component({
  selector: 'app-register-seller',
  templateUrl: './register-seller.component.html',
  styleUrls: ['./register-seller.component.scss'],
  standalone: false
})
export class RegisterSellerComponent implements OnInit, OnDestroy {
  currentStep = 0;
  registrationSteps: MenuItem[] = [];
  registerForm: FormGroup;
  isSubmitted = false;
  private authStatusSub?: Subscription;
  @ViewChild('googleLoginButton') googleBtn!: ElementRef;
  private supplierService = inject(SupplierService);

  businessTypes = [
    { name: 'Sole Proprietorship', code: 'sole_prop' },
    { name: 'Partnership', code: 'partnership' },
    { name: 'Limited Liability Company (LLC)', code: 'llc' },
    { name: 'Corporation', code: 'corp' },
    { name: 'Non-profit Organization', code: 'non_profit' }
  ];

  countries = [
    { name: 'United States', code: 'us' },
    { name: 'Canada', code: 'ca' },
    { name: 'United Kingdom', code: 'uk' },
    { name: 'Australia', code: 'au' },
    { name: 'Germany', code: 'de' },
    { name: 'France', code: 'fr' },
    { name: 'Japan', code: 'jp' },
    { name: 'Turkey', code: 'tr' }
  ];

  productCategories = [
    { name: 'Electronics', code: 'electronics' },
    { name: 'Clothing & Accessories', code: 'clothing' },
    { name: 'Home & Kitchen', code: 'home' },
    { name: 'Beauty & Personal Care', code: 'beauty' },
    { name: 'Books & Media', code: 'books' },
    { name: 'Toys & Games', code: 'toys' },
    { name: 'Sports & Outdoors', code: 'sports' },
    { name: 'Handmade Products', code: 'handmade' },
    { name: 'Food & Grocery', code: 'food' }
  ];

  priceRanges = [
    { name: 'Under $25', code: 'under_25' },
    { name: '$25 - $50', code: '25_50' },
    { name: '$50 - $100', code: '50_100' },
    { name: '$100 - $250', code: '100_250' },
    { name: '$250 - $500', code: '250_500' },
    { name: 'Over $500', code: 'over_500' }
  ];

  shippingMethods = [
    { name: 'Self-fulfilled shipping', code: 'self' },
    { name: 'Marketplace fulfillment', code: 'marketplace' },
    { name: 'Both options', code: 'both' }
  ];

  constructor(
    private fb: FormBuilder,
    private supplierRegistrationService: SupplierRegistrationService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private googleAuthService: SocialAuthService,
    private authService: AuthInterceptorService,
    private toastr: CustomToastrService,
  ) {
    this.registerForm = this.fb.group({
      businessName: ['', Validators.required],
      businessType: [null, Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^\(\d{3}\) \d{3}-\d{4}$/)]],
      taxId: ['', Validators.required],
      businessAddress: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      postalCode: ['', Validators.required],
      country: [null, Validators.required],
      productCategories: [[], Validators.required],
      productDescription: ['', Validators.required],
      averagePrice: [null, Validators.required],
      shippingMethod: [null, Validators.required],
      termsAgreed: [false, Validators.requiredTrue],
      marketingAgreed: [false]
    });
  }

  ngOnInit() {
    this.checkSupplier();
    this.registrationSteps = [
      { label: 'Basic Information' },
      { label: 'Business Details' },
      { label: 'Product Information' },
      { label: 'Review & Submit' }
    ];

    // Check if user is already logged in
    if (!this.authService.isAuthenticated) {
      // Store current url as return URL and redirect to login
      this.router.navigate(['/login'], {
        queryParams: {
          returnUrl: this.router.url
        }
      });
    }

    // Listen for Google authentication state
    this.authStatusSub = this.googleAuthService.authState.subscribe((user: SocialUser) => {
      if (user) {
        console.log('Google user authenticated:', user);
        this.registerWithGoogle(user);
      }
    });
  }

  ngOnDestroy() {
    // Clean up subscription
    if (this.authStatusSub) {
      this.authStatusSub.unsubscribe();
    }
  }

  ngAfterViewInit() {
    if (this.googleBtn && this.googleBtn.nativeElement) {
      // Dynamically create the Google sign-in button
      const button = document.createElement('asl-google-signin-button');
      button.setAttribute('type', 'icon');
      button.setAttribute('size', 'medium');
      this.googleBtn.nativeElement.appendChild(button);
    }
  }

  // Register with Google account
  async registerWithGoogle(user: SocialUser) {
    // Pre-fill form with Google data
    this.registerForm.patchValue({
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email
    });

    // Could also create a specific method in the supplier service
    // to handle Google registrations if necessary
  }

  nextStep() {
    if (this.currentStep < this.registrationSteps.length - 1) {
      this.currentStep++;
      window.scrollTo(0, 0);
    }
  }

  previousStep() {
    if (this.currentStep > 0) {
      this.currentStep--;
      window.scrollTo(0, 0);
    }
  }

  submitApplication() {
    if (this.registerForm.valid) {
      console.log('Submitted application data:', this.registerForm.value);
      const supplierRegistrationRequest = new CreateSupplierRegistration(
        null,
        this.registerForm.value.businessName,
        this.registerForm.value.businessType.code, // Extracting the code
        this.registerForm.value.firstName,
        this.registerForm.value.lastName,
        this.registerForm.value.email,
        this.registerForm.value.phone,
        this.registerForm.value.taxId,
        this.registerForm.value.businessAddress,
        this.registerForm.value.city,
        this.registerForm.value.state,
        this.registerForm.value.postalCode,
        this.registerForm.value.country.code, // Extracting the code
        this.registerForm.value.productCategories.map((category: any) => category.code), // Extracting the codes
        this.registerForm.value.productDescription,
        this.registerForm.value.averagePrice.code, // Extracting the code
        this.registerForm.value.shippingMethod.code // Extracting the code
      );
      this.supplierRegistrationService.createSupplierRegistration(supplierRegistrationRequest);
      this.toastr.message('Your application has been submitted successfully!', 'Success', {
        messageType: ToastrMessageType.Success,
        position: ToastrPosition.TopRight
      })
    } else {
      console.log('Form is invalid');
    }
  }
  async checkSupplier() {
    try {
      const supplier = await this.supplierService.read();
      console.log('Suppliers:', !!supplier);
      if (!!supplier) {
        this.isSubmitted = true;
      }
    } catch (e) {
      console.error('Error checking supplier status:', e);
    }
  }
}
