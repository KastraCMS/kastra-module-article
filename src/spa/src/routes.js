import React from 'react';
import { Switch, Route } from 'react-router-dom'
import Articles from './views/articles';
import Article from './views/article';

export const Routes = () => (
  <Switch>
    <Route exact path='/admin/module/settings/:moduleId/settings' component={Articles}/>
    <Route path='/admin/module/settings/:moduleId/settings/article/:articleId?' component={Article}/>
  </Switch>
)