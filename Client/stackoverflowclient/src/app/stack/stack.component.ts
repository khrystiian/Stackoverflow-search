import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import Keyboard from "simple-keyboard";
import { MatRadioChange } from '@angular/material';
import { map, startWith } from 'rxjs/operators';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { SearchModel } from '../shared/models/SearchModel';
import { StackService } from '../shared/services/stack.service';

@Component({
  selector: 'app-stack',
  templateUrl: './stack.component.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./stack.component.css']
})
export class StackComponent implements OnInit  {
  searchResults: any[];
  dt: Date;
  showKeyboard: boolean;
  searchModel: SearchModel;
  value = "";
  keyboard: Keyboard;
  title = 'Stackoverflow search engine';
  orderList: string[] = ['Ascending', 'Descending'];
  sortList: string[] = ['Last activity date', 'Votes', 'Creation date', 'Relevance'];
  optionList: string[] = ['Related to', 'Advanced search', 'Required', 'Synonyms'];

  stateCtrl = new FormControl();
  

  constructor(private sService: StackService) {
    this.dt = new Date();
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
      preventMouseDownDefault: true,
      onChange: input => this.onChange(input),
      onKeyPress: button => this.onKeyPress(button)
    });
  }

  onChange = (input: string) => {
    this.value = input;
    this.searchModel.userInput = this.value;
    document.getElementById("searchInput").focus();
    this._filterStates(this.value);
  };

  onKeyPress = (button: string) => {
    document.getElementById("searchInput").focus();
     //If you want to handle the shift and caps lock buttons
    if (button === "{shift}" || button === "{lock}") this.handleShift();
  };

  onInputChange = (event: any) => {
    this.keyboard.setInput(event.target.value);
    this.searchModel.userInput = event.target.value;

    document.getElementById("searchInput").focus();
    this._filterStates(event.target.value);
  };

  handleShift = () => {
    let currentLayout = this.keyboard.options.layoutName;
    let shiftToggle = currentLayout === "default" ? "shift" : "default";

    this.keyboard.setOptions({
      layoutName: shiftToggle
    });
  };



  //section virtual keyboard
  openVirtualKeyboard() {
    this.showKeyboard = !this.showKeyboard;
    const el: HTMLElement = document.getElementById("simpleVirtualKeyboard");
    if (this.showKeyboard) {
      el.hidden = false;
    } else {
      el.hidden = true;
    }
    document.getElementById("searchInput").focus();
  }
  //end section virtual keyboard


  //section search bar typing
  private _filterStates(value: string) {
    this.value = value;
    this.sService.search(this.searchModel).subscribe(response => {
      this.searchResults = response.hits.hits;
      //console.log(this.searchResults)
    });
  }
  //end section search typing


  //section search options
  orderChange($event: MatRadioChange) {
    this.searchModel.orderResults = $event.value;
  }
  sortChange($event: MatRadioChange) {
    this.searchModel.sortResults = $event.value;
  }
  optionChange($event: MatRadioChange) {
    this.searchModel.searchOption = $event.value;
  }

  creationDate(event: Date) {
    this.searchModel.creationDate = event;
  }
  //end section search options 

}
