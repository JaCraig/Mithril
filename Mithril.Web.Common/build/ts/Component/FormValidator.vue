
<template>
    <div v-show="errorMessages.length > 0" class="panel error validator" v-cloak>
      <a name="errorSection"></a>
      <header><slot>Some Errors Were Discovered</slot></header>
      <ul>
          <li v-for="errorMessage in errorMessages" v-bind:key="errorMessage">{{ errorMessage }}</li>
      </ul>
  </div>
  </template>
  
  <script lang="ts">
  import Vue from "vue";
  import { FormValidation } from "../Framework/Validation/FormValidation";
  
  export default Vue.defineComponent({
      data() {
          return {
              errorMessages: <string[]>[],
              formValidator: new FormValidation()
          };
      },
      mounted() {
          this.$nextTick(function () {
              this.revalidate();
          });
      },
      methods: {
          revalidate: function () {
              if (this.$el == null) {
                  return true;
              }
              let FormElement = this.getParentForm(this.$el);
              if(FormElement == null && !this.formValidator.validate()) {
                  this.errorMessages = this.formValidator.errors;
                  return false;
              }
              let Errors = this.formValidator.validateForm(FormElement);
              if (Errors.length > 0) {
                  this.errorMessages = Errors;
                  return false;
              }
              this.errorMessages = [];
              return true;
          },
          getParentForm: function (element: any) {
              let CurrentParent = element.parentNode;
              if (CurrentParent.nodeName === "FORM" || CurrentParent === null) {
                  return CurrentParent;
              } else {
                  return this.getParentForm(CurrentParent);
              }
          },
      }
  });
  </script>