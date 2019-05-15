export class ResultModel {
  hit: Hits;
}

export class Total {
  value: number;
}

export class Hits {
  total: Total;
  hits: Hit[];
}

export class Hit {
  _source: any;
}
