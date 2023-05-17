import { Component, OnInit, TemplateRef, Inject, LOCALE_ID } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Person } from './person';
import { PersonService } from 'src/app/services/person/person.service';
import {  formatDate } from '@angular/common';
import { Region } from './region';
import { Color } from './enums/color';
import { Schooling } from './enums/schooling';
import { FamilyTree } from './familytree';

@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.css']
})

export class PersonComponent implements OnInit{

  
  FormatDateStr(dateStr :any): string{
    return formatDate(dateStr, 'dd/MM/yyyy' ,this.locale);
}

FormatDateTimeStr(dateStr :any): string{
  return formatDate(dateStr, 'dd/MM/yyyy HH:mm:ss' ,this.locale);
}

  constructor(@Inject(LOCALE_ID) public locale: string,private PersonService: PersonService,
    private modalService: BsModalService) {}

   
  form: any;
  formFilter: any;
  formFamilyTree: any;
  percentualView: any;
  familyTree: FamilyTree | undefined;
  visiblePercentual: boolean = false; 
  level: number | undefined;
  formRegion: any;
  titleForm: string | undefined;
  persons: Person[] | undefined;
  person: Person | undefined;
  region: Region | undefined;
  title: string | undefined;
  personId: string | undefined;
  description: string | undefined;
  startDate: Date | undefined;
  expirateDate: Date | undefined;
  visibleTable: boolean = true;
  visibleForm: boolean = false; 
  visiblePersons: boolean =false;
  visiblePerson: boolean =false;
  visibleFamilyTree: boolean =false;
  
  modalRef: BsModalRef | any;

  ngOnInit(): void {
    this.ShowFormFilter();
    this.PersonService.GetAll().subscribe((result) => {
      this.persons = result;
    });
  }

 
  ShowPerson(personId :any): void {
    this.visibleTable = false;
     this.visiblePerson = true;
     this.PersonService.GetById(personId).subscribe((result) => {
       this.person = result;
       this.ShowFormFamilyTree(this.person.personId);
     });
    
    }

    SendFamilyTree(): void {
      this.visibleTable = false;
       this.visiblePerson = true;
       const level = this.formFamilyTree.value.level;
       const personId = this.formFamilyTree.value.personId;
       this.PersonService.GetFamilyTree(personId, level).subscribe((result) => {
        console.log("result",result) 
        this.familyTree = result;
         this.visibleFamilyTree = true;
         
         console.log(this.familyTree)
       });
      
      }
      ShowFormFamilyTree(personId: string | undefined): void {
        this.formFamilyTree = new FormGroup({
          personId : new FormControl(personId),
          level: new FormControl(null),
        });
    }


      ShowFormFilter(): void {
        this.formFilter = new FormGroup({
          region: new FormGroup({
            continent: new FormControl(null),
            zipCode: new FormControl(null),
            country: new FormControl(null),
            state: new FormControl(null),
            city: new FormControl(null),
            street: new FormControl(null),
            neighborhood: new FormControl(null),
          }),
          name: new FormControl(null),
          document: new FormControl(null),
          documentFather: new FormControl(null),
          documentMother: new FormControl(null),
          color: new FormControl(null),
          motherName: new FormControl(null),
          fatherName: new FormControl(null),
          schooling: new FormControl(null),
          pageNumber: new FormControl(1),
          pageSize: new FormControl(10),
          withPencentual: new FormControl(null),
        });
      }

  ShowFormRegister(): void {
    this.visibleTable = false;
    this.visibleForm = true;
    this.visiblePersons = false;
    this.titleForm = 'New Person';
    this.form = new FormGroup({
      region: new FormGroup({
        continent: new FormControl(null),
        zipCode: new FormControl(null),
        country: new FormControl(null),
        state: new FormControl(null),
        city: new FormControl(null),
        street: new FormControl(null),
        neighborhood: new FormControl(null),
      }),
      name: new FormControl(null),
      document: new FormControl(null),
      documentFather: new FormControl(null),
      documentMother: new FormControl(null),
      color: new FormControl(null),
      motherName: new FormControl(null),
      fatherName: new FormControl(null),
      schooling: new FormControl(null),
    });
  }

  ShowFormUpdate(personId :any): void {
    this.visibleTable = false;
    this.visibleForm = true;
    this.visiblePersons = false;
    this.PersonService.GetById(personId).subscribe((result) => {
      this.titleForm = `Update ${result.name} - ${result.document}`;
      this.form = new FormGroup({
        personId: new FormControl(result.personId),
        name: new FormControl(result.name),
        document: new FormControl(result.document),
        documentFather: new FormControl(result.documentFather),
        documentMother: new FormControl(result.documentMother),
        color: new FormControl(result.color),
        motherName: new FormControl(result.motherName),
        fatherName: new FormControl(result.fatherName),
        schooling: new FormControl(result.schooling),
        region: new FormGroup({
          continent: new FormControl(result.region?.continent),
          zipCode: new FormControl(result.region?.zipCode),
          country: new FormControl(result.region?.country),
          state: new FormControl(result.region?.state),
          city: new FormControl(result.region?.city),
          street: new FormControl(result.region?.street),
          neighborhood: new FormControl(result.region?.neighborhood),
        }),
      });
    });
  }

  SendForm(): void {
    const person: Person = this.form.value;
    console.log("sendForm: ", person);

    if (person.personId != null) {
      console.log("entrou no update");
      this.PersonService.Update(person).subscribe((result) => {
        this.visibleForm = false;
        this.visibleTable = true;
        alert('Pessoa atualizada com sucesso');
        this.PersonService.GetAll().subscribe((registers) => {
          this.persons = registers;
        });
      });
    } else {
      this.PersonService.Create(person).subscribe((result) => {
        this.visibleForm = false;
        this.visibleTable = true;
        alert('Person created with success');
        this.PersonService.GetAll().subscribe((registers) => {
          this.persons = registers;
        });
      });
    }
  }



  RegionIsValid(region: Region | undefined): Boolean{
    if(region?.continent == null && region?.zipCode == null && 
      region?.street == null && region?.neighborhood == null && 
      region?.city == null && region?.state == null &&
      region?.country == null){
        return false;
    }
    return true;
  }
  SendFilter(): void {
    
    const person: Person = this.formFilter.value;
    var pageNumber = this.formFilter.value.pageNumber;
    var pageSize = this.formFilter.value.pageSize;
    var withPencentual = this.formFilter.value.withPencentual;
    console.log("Person: ",person)
    console.log("withPencentual: ", withPencentual)
    
    if(pageNumber == null || pageSize == null) return alert("Page Number and Page Size required to find!");
    console.log("Region: ", this.RegionIsValid(person.region))
    if(withPencentual && this.RegionIsValid(person.region)){
      this.PersonService.GetPercentualByRegion(person).subscribe((percentual) => {
        console.log(percentual);
        this.percentualView = percentual;
      });
      this.PersonService.GetByFilter(person, pageNumber, pageSize).subscribe((registers) => {
        console.log(registers);
        this.persons = registers.list;
      });
      this.visiblePercentual = true;
    }else{
      this.PersonService.GetByFilter(person, pageNumber, pageSize).subscribe((registers) => {
        console.log(registers);
        this.persons = registers.list;
        this.visiblePercentual = false;
      });
    }
   
      
  }

  Back(): void {
    this.visibleTable = true;
    this.visibleForm = false;
    this.visiblePerson = false;
  }

  ShowConfirmDelete(personId: any, name: any, contentModal: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(contentModal);
    this.personId = personId;
    this.title = name;
  }

  Delete(personId: any){
    this.PersonService.Delete(personId).subscribe(result => {
      this.modalRef.hide();
      alert('Person deleted with success');
      this.PersonService.GetAll().subscribe(resultGetAll => {
        this.persons = resultGetAll;
      });
    });
  }

  // Selects
  continents = [
    {name: '', value: null},
    {name: 'Africa', value: 0},
    {name: 'Antarctica', value: 1},
    {name: 'Asia', value: 2},
    {name: 'Europe', value: 3},
    {name: 'North America', value: 4},
    {name: 'Oceania', value: 5},
    {name: 'South America', value: 6},
  ];

  schoolings = [
    {name: '', value: null},
    {name: 'Illiterate', value: 0},
    {name: 'Incomplete Primary Education', value: 1},
    {name: 'Complete Primary Education', value: 2},
    {name: 'Incomplete High School', value: 3},
    {name: 'Complete High School', value: 4},
    {name: 'Incomplete Higher', value: 5},
    {name: 'Graduated', value: 6},
    {name: 'Post Graduate', value: 7},
    {name: 'Masters Degree', value: 8},
    {name: 'Doctorate Degree', value: 9},

  ];

  colors = [
    {name: '', value: null},
    {name: 'Black', value: 0},
    {name: 'White', value: 1},
    {name: 'Brown', value: 2},
    {name: 'Indigenous', value: 3},
    {name: 'Yellow', value: 4},
  ];

  // -- CONVERSIONS
  ColorToString(color: Color | undefined): string{
    switch (color) {
      case Color.Black:
        return 'Black';
      case Color.White:
        return 'White';
      case Color.Brown:
        return 'Brown';
      case Color.Indigenous:
        return 'Indigenous';
      case Color.Yellow:
        return 'Yellow';
      default:
        return '';
    }
  }
  
  SchoolingToString(color: Schooling | undefined): string{
    switch (color) {
      case Schooling.Illiterate:
        return 'Illiterate';
      case Schooling.IncompletePrimaryEducation:
        return 'Incomplete Primary Education';
      case Schooling.CompletePrimaryEducation:
        return 'Complete Primary Education';
      case Schooling.IncompleteHighSchool:
        return 'Incomplete High School';
      case Schooling.CompleteHighSchool:
        return 'Complete High School';
      case Schooling.IncompleteHigher:
        return 'Incomplete Highers';
      case Schooling.Graduated:
        return 'Graduated';
      case Schooling.PostGraduate:
        return 'Post Graduate';
      case Schooling.MastersDegree:
        return 'Masters Degree';
      case Schooling.DoctorateDegree:
        return 'Doctorate Degree';    
      default:
        return '';
    }
  }
}

