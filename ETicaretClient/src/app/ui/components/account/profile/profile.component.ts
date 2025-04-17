import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserProfile } from '../../../../contracts/users/userProfile';
import { UserService } from '../../../../services/common/models/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
  standalone: false
})
export class ProfileComponent implements OnInit {
  user: UserProfile;
  currentSection = 'profile';
  displayEditDialog = false;
  profileForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.profileForm = this.fb.group({
      username: ['', Validators.required],
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['']
    });
  }

  private userService = inject(UserService);

  ngOnInit(): void {
    // Burada kullanıcı bilgilerini API'den alacağınız servis çağrısı olacak
    this.loadUserData();
  }

  async loadUserData() {
    this.user = await this.userService.getProfile();
    console.log('Kullanıcı bilgileri yüklendi:', this.user);
    this.user.twoFactorEnabled = true;
  }

  openEditDialog() {
    this.profileForm.patchValue({
      username: this.user.username,
      name: this.user.name,
      email: this.user.email,
      phoneNumber: this.user.phoneNumber
    });
    this.displayEditDialog = true;
  }

  updateProfile() {
    if (this.profileForm.valid) {
      const formData = this.profileForm.value;

      // API'ye güncelleme isteği göndermeden önce sadece değiştirilebilir alanları alın
      const updatedData = {
        name: formData.name,
        phone: formData.phoneNumber
      };

      // Burada gerçek API çağrısı yapılacak
      this.userService.updateProfile(updatedData.phone, updatedData.name)
      console.log('Profil güncellendi:', updatedData);

      // Başarılı yanıt gelirse kullanıcı verisini güncelle
      this.user = {
        ...this.user,
        ...updatedData,
        lastUpdated: new Date()
      };

      this.displayEditDialog = false;
    }
  }

  getSubscriptionClass() {
    const type = this.user?.subscriptionType?.toLowerCase();
    return type || 'standard';
  }
}
