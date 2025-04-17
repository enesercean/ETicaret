import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AppModule } from './app.module';
import { provideServerRendering } from '@angular/platform-server';
import { serverRoutes } from './app.routes.server';

@NgModule({
  imports: [AppModule],
  providers: [provideServerRendering()],
  bootstrap: [AppComponent],
})
export class AppServerModule { }
