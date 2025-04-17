import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

// PrimeNG components
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { AccordionModule } from 'primeng/accordion';
import { SelectModule } from 'primeng/select';
import { CheckboxModule } from 'primeng/checkbox';
import { TextareaModule } from 'primeng/textarea';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { DividerModule } from 'primeng/divider';
import { CardModule } from 'primeng/card';
import { TooltipModule } from 'primeng/tooltip';
import { RippleModule } from 'primeng/ripple';
import { HelpComponent } from './help.component';

@NgModule({
  declarations: [
    HelpComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild([
      { path: '', component: HelpComponent }
    ]),
    InputTextModule,
    ButtonModule,
    AccordionModule,
    SelectModule,
    CheckboxModule,
    TextareaModule,
    ScrollPanelModule,
    DividerModule,
    CardModule,
    TooltipModule,
    RippleModule
  ],
  exports: [
    HelpComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]

})
export class HelpModule { }
