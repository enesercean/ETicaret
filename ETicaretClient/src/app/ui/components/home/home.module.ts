import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CarouselModule } from 'primeng/carousel';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { TagModule } from 'primeng/tag';
import { GalleriaModule } from 'primeng/galleria';
import { BadgeModule } from 'primeng/badge';
import { ImageModule } from 'primeng/image';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { StyleClassModule } from 'primeng/styleclass';
import { DividerModule } from 'primeng/divider';
import { RippleModule } from 'primeng/ripple';
import { ChipModule } from 'primeng/chip';
import { CardModule } from 'primeng/card';
import { SliderModule } from 'primeng/slider';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { TooltipModule } from 'primeng/tooltip';
import { MenubarModule } from 'primeng/menubar';
import { DialogModule } from 'primeng/dialog';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    ButtonModule,
    RouterModule.forChild([
      { path: "", component: HomeComponent }
    ]),
    CarouselModule,
    RatingModule,
    FormsModule,
    InputTextModule,
    ProgressSpinnerModule,
    TagModule,
    GalleriaModule,
    BadgeModule,
    ImageModule,
    ToastModule,
    StyleClassModule,
    DividerModule,
    RippleModule,
    ChipModule,
    CardModule,
    SliderModule,
    OverlayPanelModule,
    TooltipModule,
    MenubarModule,
    DialogModule
  ],
  providers: [
    MessageService
  ],
  exports: [
    HomeComponent
  ]
})
export class HomeModule { }
