import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import Keyboard from "simple-keyboard";
import { MatRadioChange } from '@angular/material';
import { map, startWith } from 'rxjs/operators';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { SearchModel } from '../shared/models/SearchModel';

export interface State {
  flag: string;
  name: string;
  population: string;
}
@Component({
  selector: 'app-stack',
  templateUrl: './stack.component.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./stack.component.css']
})
export class StackComponent implements OnInit {
  dt: Date;
  showKeyboard: boolean;
  searchModel: SearchModel;
  value = "";
  keyboard: Keyboard;
  title = 'Stackoverflow search engine';
  orderList: string[] = ['Ascending', 'Descending'];
  sortList: string[] = ['Last activity date', 'Votes', 'Creation date', 'Relevance'];
  optionList: string[] = ['Related to', 'Advanced search', 'Required', 'Synonyms'];
  diacritics: any = {
    a: 'ÀÁÂÃÄÅàáâãäåĀāąĄ',
    c: 'ÇçćĆčČ',
    d: 'đĐďĎ',
    e: 'ÈÉÊËèéêëěĚĒēęĘ',
    i: 'ÌÍÎÏìíîïĪī',
    l: 'łŁ',
    n: 'ÑñňŇńŃ',
    o: 'ÒÓÔÕÕÖØòóôõöøŌō',
    r: 'řŘ',
    s: 'ŠšśŚȘș',
    t: 'ťŤȚț',
    u: 'ÙÚÛÜùúûüůŮŪū',
    y: 'ŸÿýÝ',
    z: 'ŽžżŻźŹ'
  }

  stateCtrl = new FormControl();
  filteredStates: Observable<State[]>;

  states: State[] = [
    {
      name: 'Arkansas',
      population: '2.978M',
      // https://commons.wikimedia.org/wiki/File:Flag_of_Arkansas.svg
      flag: 'https://upload.wikimedia.org/wikipedia/commons/9/9d/Flag_of_Arkansas.svg'
    },
    {
      name: 'California',
      population: '39.14M',
      // https://commons.wikimedia.org/wiki/File:Flag_of_California.svg
      flag: 'https://upload.wikimedia.org/wikipedia/commons/0/01/Flag_of_California.svg'
    },
    {
      name: 'Florida',
      population: '20.27M',
      // https://commons.wikimedia.org/wiki/File:Flag_of_Florida.svg
      flag: 'https://upload.wikimedia.org/wikipedia/commons/f/f7/Flag_of_Florida.svg'
    },
    {
      name: 'Texas',
      population: '27.47M',
      // https://commons.wikimedia.org/wiki/File:Flag_of_Texas.svg
      flag: 'https://upload.wikimedia.org/wikipedia/commons/f/f7/Flag_of_Texas.svg'
    },
    {
      name: 'Florida',
      population: '20.27M',
      // https://commons.wikimedia.org/wiki/File:Flag_of_Florida.svg
      flag: 'https://upload.wikimedia.org/wikipedia/commons/f/f7/Flag_of_Florida.svg'
    },
    {
      name: 'Texas',
      population: '27.47M',
      // https://commons.wikimedia.org/wiki/File:Flag_of_Texas.svg
      flag: 'https://upload.wikimedia.org/wikipedia/commons/f/f7/Flag_of_Texas.svg'
    }
  ];


  constructor() {
    this.dt = new Date();
    this.filteredStates = this.stateCtrl.valueChanges
      .pipe(
        startWith(''),
        map(state => state ? this._filterStates(state) : this.states.slice())
    );
  }

  ngOnInit() {
    this.searchModel = {
      orderResults: this.orderList[0],
      sortResults: this.sortList[0],
      searchOption: this.optionList[1],
      creationDate: null,
      userInput: null
    };
    this.showKeyboard = false;

    this.keyboard = new Keyboard({
      onChange: input => {
        this.onChange(input)
      },
      onKeyPress: button => this.onKeyPress(button)
    });
  }


  //section virtual keyboard
  openVirtualKeyboard() {
    this.showKeyboard = !this.showKeyboard;
    const el: HTMLElement = document.getElementById("simpleVirtualKeyboard");

    if (this.showKeyboard) {
      el.hidden = false;
    } else {
      el.hidden = true;
    }
  }

  onChange = (input: string) => {
    this.value = input;
    console.log("Input changed", input);
  };
  onKeyPress = (button: string) => {
    console.log("Button pressed", button);

     //If you want to handle the shift and caps lock buttons
    if (button === "{shift}" || button === "{lock}") this.handleShift();
  };
  handleShift = () => {
    let currentLayout = this.keyboard.options.layoutName;
    let shiftToggle = currentLayout === "default" ? "shift" : "default";

    this.keyboard.setOptions({
      layoutName: shiftToggle
    });
  };
  //end section virtual keyboard


  //section search bar typing
  private _filterStates(value: string): State[] {
    const filterValue = value.toLowerCase();
    //Call the service
      //TO DO -- call the backend

    return this.states.filter(state => state.name.toLowerCase().indexOf(filterValue) === 0);
  }
  //end section search typing


  //section search options
  orderChange($event: MatRadioChange) {
    this.searchModel.orderResults = $event.value;
    console.log(this.searchModel)
  }
  sortChange($event: MatRadioChange) {
    this.searchModel.sortResults = $event.value;
    console.log(this.searchModel)
  }
  optionChange($event: MatRadioChange) {
    this.searchModel.searchOption = $event.value;
    console.log(this.searchModel)
  }

  creationDate(event: Date) {
    this.searchModel.creationDate = event;
    console.log(this.searchModel)
  }
  onInputChange($event: any) {
    this.searchModel.userInput = $event.target.value;
    console.log(this.searchModel)
  }
  //end section search options 

}
