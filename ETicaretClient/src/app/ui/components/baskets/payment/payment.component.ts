import { Component, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';
import { CreatePayment } from '../../../../contracts/payment/createPayment';


@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss'],
  standalone: false
})
export class PaymentComponent {
  @Output() paymentValid = new EventEmitter<boolean>(); // Formun geçerli olup olmadığını gönderir
  @Output() paymentSaved = new EventEmitter<CreatePayment>();
  paymentForm: FormGroup;
  installmentOptions = [
    { label: '1 Installment', value: '1' },
    { label: '2 Installments', value: '2' },
    { label: '3 Installments', value: '3' },
    { label: '6 Installments', value: '6' },
    { label: '12 Installments', value: '12' }
  ];

  constructor(
    private fb: FormBuilder,
    private toastr: CustomToastrService // Toastr servisini enjekte edin
  ) {
    this.paymentForm = this.fb.group({
      cardNumber: ['', [Validators.required, Validators.pattern(/^\d{16}$/)]], // 16 haneli kart numarası
      cardHolderName: ['', Validators.required],
      expiryDate: ['', [Validators.required, Validators.pattern(/^(0[1-9]|1[0-2])\/\d{2}$/)]], // MM/YY formatı
      cvv: ['', [Validators.required, Validators.pattern(/^\d{3}$/)]], // 3 haneli CVV
      installment: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  // Formun geçerli olup olmadığını kontrol eden metod
  isFormValid(): boolean {
    return this.paymentForm.valid;
  }

  // Belirli bir alanın geçerli olup olmadığını kontrol eden metod
  isInvalid(field: string): boolean {
    const control = this.paymentForm.get(field);
    return control?.invalid && (control?.dirty || control?.touched);
  }

  // Ödeme bilgilerini doğrulayan ve parent component'e gönderen metod
  savePayment() {
    if (this.isFormValid()) {
      const formValue = this.paymentForm.value; // Form değerlerini al
      const paymentData = new CreatePayment(
        formValue.cardNumber,
        formValue.cardHolderName,
        formValue.expiryDate,
        formValue.cvv,
        formValue.installment,
        formValue.email
      );
      this.paymentValid.emit(true); // Form geçerliyse parent component'e true gönder
      this.paymentSaved.emit(paymentData); // Ödeme bilgilerini gönder
    } else {
      this.paymentValid.emit(false); // Form geçersizse parent component'e false gönder
      this.paymentSaved.emit(null); // Ödeme bilgileri geçersizse null gönder
      this.toastr.message('Lütfen tüm ödeme alanlarını doğru şekilde doldurun ve kaydedin.', 'Geçersiz Ödeme', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    }
  }
}
