import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainPageComponent } from './main-page/main-page.component';
import { ProfileModule } from './profile/profile.module';

@NgModule({
    declarations: [MainPageComponent],
    imports: [CommonModule, ProfileModule],
})
export class GeneralModule {}
