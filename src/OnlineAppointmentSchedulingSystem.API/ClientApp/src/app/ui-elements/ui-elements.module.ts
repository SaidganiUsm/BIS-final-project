import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogPopupComponent } from './dialog-popup/dialog-popup.component';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
    declarations: [DialogPopupComponent],
    imports: [
        CommonModule,
        FormsModule,
        MatButtonModule,
        RouterModule,
        MatIconModule,
    ],
    exports: [],
})
export class UiElementsModule {}
