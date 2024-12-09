import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from '../src/app/app.component';
import { provideRouter } from '@angular/router';
import {appRoutes} from '../src/app/app.routes';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import {HTTP_INTERCEPTORS, provideHttpClient, withInterceptors} from '@angular/common/http';
import { authInterceptor } from './app/interceptors/auth.interceptor';

bootstrapApplication(AppComponent,{
  providers: [
    [provideHttpClient(withInterceptors([authInterceptor]))],
    provideRouter(appRoutes),
    BrowserAnimationsModule,
    ReactiveFormsModule,
    {provide: HTTP_INTERCEPTORS, useExisting: authInterceptor, multi: true}
  ]
}).catch((err) => console.error(err));