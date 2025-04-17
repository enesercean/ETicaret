import { Component } from '@angular/core';

@Component({
  selector: 'app-help',
  templateUrl: './help.component.html',
  styleUrls: ['./help.component.scss'],
  standalone: false
})
export class HelpComponent {
  searchQuery: string = '';
  activeSection: string = '';
  contactForm: any = {
    name: '',
    email: '',
    subject: '',
    message: '',
    privacyAgreed: false
  };
  contactSubjects: any[] = [
    { label: 'Genel Soru', value: 'general' },
    { label: 'Sipariş Sorunu', value: 'order' },
    { label: 'İade ve İptal', value: 'return' },
    { label: 'Kargo ve Teslimat', value: 'shipping' },
    { label: 'Ödeme Sorunu', value: 'payment' },
    { label: 'Hesap Sorunu', value: 'account' },
    { label: 'Diğer', value: 'other' }
  ];

  searchHelp() {
    // Arama işlemi için gerekli kodlar
    console.log('Arama yapılıyor:', this.searchQuery);
  }

  scrollTo(section: string) {
    this.activeSection = section;
    document.getElementById(section)?.scrollIntoView({ behavior: 'smooth' });
  }

  startLiveChat() {
    // Canlı destek başlatma işlemi için gerekli kodlar
    console.log('Canlı destek başlatılıyor');
  }

  createSupportTicket() {
    // Destek talebi oluşturma işlemi için gerekli kodlar
    console.log('Destek talebi oluşturuluyor');
  }

  sendContactForm() {
    if (this.contactForm.name && this.contactForm.email && this.contactForm.subject && this.contactForm.message && this.contactForm.privacyAgreed) {
      // İletişim formu gönderme işlemi için gerekli kodlar
      console.log('İletişim formu gönderiliyor:', this.contactForm);
    } else {
      console.log('Lütfen tüm alanları doldurun ve kişisel verilerinizin işlenmesine izin verin.');
    }
  }
}
