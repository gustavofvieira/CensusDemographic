import { Component, OnInit, TemplateRef, Inject, LOCALE_ID } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Census } from './census';
import { PersonService } from 'src/app/services/person/person.service';
import {  formatDate } from '@angular/common';

@Component({
  selector: 'app-census',
  templateUrl: './census.component.html',
  styleUrls: ['./census.component.css']
})

export class CensusComponent implements OnInit{

  
  FormatDateStr(dateStr :any): string{
    return formatDate(dateStr, 'dd/MM/yyyy' ,this.locale);
}

FormatDateTimeStr(dateStr :any): string{
  return formatDate(dateStr, 'dd/MM/yyyy HH:mm:ss' ,this.locale);
}

  constructor(@Inject(LOCALE_ID) public locale: string,private PersonService: PersonService,
    private modalService: BsModalService) {}

   
  form: any;
  titleForm: string | undefined;
  censusList: Census[] | undefined;
  census: Census| undefined;
  title: string | undefined;
  personId: string | undefined;
  description: string | undefined;
  startDate: Date | undefined;
  expirateDate: Date | undefined;
  candidateIds: string[] | undefined;
  visibleTable: boolean = true;
  visibleForm: boolean = false; 
  visibleCandidates: boolean =false;
  
  modalRef: BsModalRef | any;

  ngOnInit(): void {
    this.PersonService.GetAll().subscribe((result) => {
      console.log(result)
      // this.censusList = result;
    });
  }
 
  ShowPerson(personId :any): void {
    this.visibleTable = false;
    this.visibleCandidates = true;
    //  this.titleForm = `Update ${result.title} ${result.createdAt}`;
     this.PersonService.GetById(personId).subscribe((result) => {
      //  this.census = result;
     });
    
    }

//   ShowFormRegister(): void {
//     this.visibleTable = false;
//     this.visibleForm = true;
//     this.visibleCandidates = false;
//     this.titleForm = 'Census';
//     this.form = new FormGroup({
//       title: new FormControl(null),
//       description: new FormControl(null),
//       startDate: new FormControl(null),
//       expirateDate: new FormControl(null),
//       candidateIds: new FormControl([]),
//     });
//   }

  ShowFormUpdate(personId :any): void {
    this.visibleTable = false;
    this.visibleForm = true;
    this.visibleCandidates = false;
    this.PersonService.GetById(personId).subscribe((result) => {
      this.titleForm = `Update ${result.name} - ${result.createdAt}`;
      this.form = new FormGroup({
        personId: new FormControl(result.personId),
        name: new FormControl(result.name),
        color: new FormControl(result.color),
        motherName: new FormControl(result.motherName),
        fatherName: new FormControl(result.fatherName),
        schooling: new FormControl(result.schooling),
        // region: new FormControl(result.region),
      });
    });
  }

//   SendForm(): void {
//     const person: Census = this.form.value;

//     if (person.personId != null) {
//       console.log("entrou no update");
//       this.PersonService.Update(person).subscribe((result) => {
//         this.visibleForm = false;
//         this.visibleTable = true;
//         alert('Pessoa atualizada com sucesso');
//         this.PersonService.GetAll().subscribe((registers) => {
//           // this.censusList = registers;
//         });
//       });
//     } else {
//       this.PersonService.Create(person).subscribe((result) => {
//         this.visibleForm = false;
//         this.visibleTable = true;
//         alert('Person created with success');
//         this.PersonService.GetAll().subscribe((registers) => {
//           // this.censusList = registers;
//         });
//       });
//     }
//   }

  Back(): void {
    this.visibleTable = true;
    this.visibleForm = false;
    this.visibleCandidates = false;
  }

  
}

