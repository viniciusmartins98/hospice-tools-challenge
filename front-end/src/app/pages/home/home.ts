import { Component, model, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import IPatient from '../../services/patients/interfaces/patient';

@Component({
  selector: 'app-home',
  imports: [
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    FormsModule,
    MatTableModule
  ],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
  patientName = model('');
  patients = signal<IPatient[]>([{
    age: 24,
    createdAt: new Date(),
    updatedAt: new Date(),
    favoriteColor: 'Azul',
    firstName: 'João',
    lastName: 'Silva',
    fullName: 'João Silva',
    gender: 'Masculino',
    id: '1'
  }]);
  columns = ['name', 'gender', 'age', 'favoriteColor', 'actions'];

  search() {
    console.log(this.patientName());
  }
}
