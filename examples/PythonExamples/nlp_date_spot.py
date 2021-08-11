from datetime import datetime, timedelta
import jieba.posseg as psg
import hanlp

for k, v in psg.cut("2017年7月23日当天"):
    print(k, v)

# HanLP = hanlp.load(hanlp.pretrained.mtl.CLOSE_TOK_POS_NER_SRL_DEP_SDP_CON_ELECTRA_SMALL_ZH)
# doc = HanLP(["2017年7月23日当天"])
# print(doc)
